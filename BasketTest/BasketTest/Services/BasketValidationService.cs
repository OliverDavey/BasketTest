using AutoMapper;
using BasketTest.Data;
using BasketTest.Data.Models;
using BasketTest.SDK.Models;
using BasketTest.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Services
{
    public class BasketValidationService : IBasketValidationService
    {
        private readonly IStockItemRepository stockItemRepository;
        private readonly IOfferRepository offerRepository;
        private readonly IGiftCardRepository giftCardRepository;
        private readonly IMapper mapper;

        public BasketValidationService(
            IStockItemRepository stockItemRepository,
            IOfferRepository offerRepository,
            IGiftCardRepository giftCardRepository,
            IMapper mapper)
        {
            this.stockItemRepository = stockItemRepository;
            this.offerRepository = offerRepository;
            this.giftCardRepository = giftCardRepository;
            this.mapper = mapper;
        }

        public async Task<ValidationResult> ValidateCreateBasket(CreateBasketRequest basket)
        {
            if (basket == null)
            {
                return new ValidationResult
                {
                    Success = false,
                    Message = "No basket body supplied"
                };
            }

            var stockCheck = await this.CheckStock(basket.Items);
            if (!stockCheck.Success)
            {
                return stockCheck;
            }

            return new ValidationResult
            {
                Success = true
            };
        }

        private async Task<ValidationResult> CheckStock(IList<SDK.Models.CreateBasketItem> basketItems)
        {
            var groups = basketItems.GroupBy(item => item.ProductId);
            foreach (var group in groups)
            {
                var totalQuantity = group.Sum(item => item.Quantity);
                if (totalQuantity <= 0)
                {
                    return new ValidationResult
                    {
                        Success = false,
                        Message = "Invalid quantity requested"
                    };
                }

                var stockCheck = await this.stockItemRepository.CheckStock(group.First().ProductId, totalQuantity);
                if (!stockCheck.Success)
                {
                    var response = this.mapper.Map<ValidationResult>(stockCheck);
                    return response;
                }
            }

            return new ValidationResult
            {
                Success = true
            };
        }

        public async Task<ValidationResult> CheckOffer(GetBasketModel basket, string offerCode)
        {
            var offer = await this.offerRepository.GetOffer(offerCode);

            if (offer == null)
            {
                return new ValidationResult
                {
                    Success = false,
                    Message = "Offer code does not exist"
                };
            }

            // Are there items that it applies to
            var basketItems = await this.GetItems(basket);
            var itemTags = basketItems.SelectMany(item => item.Tags);

            if (offer.ApplicableItems.Any()
                && !itemTags.Intersect(offer.ApplicableItems).Any())
            {
                return new ValidationResult
                {
                    Success = false,
                    Message = $"There are no products in your basket applicable to voucher {offerCode}"
                };
            }

            // Have we met the minimum-spend threshold
            if (basket.DiscountableTotal < offer.Threshold)
            {
                var difference = offer.Threshold - basket.DiscountableTotal;
                return new ValidationResult
                {
                    Success = false,
                    Message = $"You have not reached the spend threshold for voucher {offerCode}. Spend another £{difference} to receive £{offer.Value} discount from your basket total"
                };
            }

            return new ValidationResult
            {
                Success = true
            };
        }

        public async Task<ValidationResult> CheckGiftCard(GetBasketModel basket, string voucherCode)
        {
            // Does the basket already have this same card applied?
            // Do it like this because we don't want to actually remove the card from the pool until it has been spent

            // Does the voucher exist in the store?
            var giftCard = await this.giftCardRepository.GetGiftCard(voucherCode);

            if (giftCard == null)
            {
                return new ValidationResult
                {
                    Success = false,
                    Message = "Not a valid voucher code"
                };
            }

            return new ValidationResult
            {
                Success = true
            };
        }

        private async Task<List<StockItem>> GetItems(GetBasketModel basket)
        {
            var basketItems = new List<StockItem>();
            var groups = basket.Items.GroupBy(item => item.ProductId);
            foreach (var group in groups)
            {
                var stockItem = await this.stockItemRepository.GetItem(group.First().ProductId);

                basketItems.AddRange(Enumerable.Repeat(stockItem, group.Count()));
            }

            return basketItems;
        }
    }
}
