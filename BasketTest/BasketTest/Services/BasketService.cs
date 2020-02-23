using AutoMapper;
using BasketTest.Data;
using BasketTest.Data.Models;
using BasketTest.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IStockItemRepository stockItemRepository;
        private readonly IOfferRepository offerRepository;
        private readonly IGiftCardRepository giftCardRepository;
        private readonly IMapper mapper;

        public BasketService(
            IBasketRepository basketRepository,
            IStockItemRepository stockItemRepository,
            IOfferRepository offerRepository,
            IGiftCardRepository giftCardRepository,
            IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.stockItemRepository = stockItemRepository;
            this.offerRepository = offerRepository;
            this.giftCardRepository = giftCardRepository;
            this.mapper = mapper;
        }

        public async Task<GetBasketModel> GetBasket(string id)
        {
            var basket = await this.basketRepository.Get(id);
            var result = this.mapper.Map<GetBasketModel>(basket);

            if (result == null)
            {
                return null;
            }

            var stockItems = await this.GetStockItems(basket);
            result.SubTotal = stockItems.Sum(item => item.Price);
            result.DiscountableTotal = this.GetDiscountableTotal(stockItems);

            // Offers are validated at point of basket changing. We can trust that they will still apply here
            var offerDiscount = basket.OfferCode != null
                ? (await this.offerRepository.GetOffer(basket.OfferCode)).Value
                : 0m;

            var giftCards = await this.GetGiftCards(basket);
            result.RedeemedCards = this.mapper.Map<IList<GetBasketGiftCardModel>>(giftCards);

            var giftCardTotal = giftCards.Sum(card => card.Value);

            result.TotalDiscount = Math.Min(offerDiscount + giftCardTotal, result.DiscountableTotal);
            result.TotalPrice = result.SubTotal - result.TotalDiscount;

            return result;
        }

        private async Task<List<GiftCard>> GetGiftCards(Basket basket)
        {
            var giftCards = new List<GiftCard>();
            foreach (var card in basket.GiftCards)
            {
                var giftCard = await this.giftCardRepository.GetGiftCard(card.VoucherCode);
                giftCards.Add(giftCard);
            }

            return giftCards;
        }

        private decimal GetDiscountableTotal(List<StockItem> stockItems)
        {
            var nonDiscountableTags = new List<ProductTags> { ProductTags.GiftCard };
            var result = stockItems
                .Where(item => !item.Tags.Intersect(nonDiscountableTags).Any())
                .Sum(item => item.Price);

            return result;
        }

        private async Task<List<StockItem>> GetStockItems(Basket basket)
        {
            var stockItems = new List<StockItem>();
            var groups = basket.Items.GroupBy(item => item.ProductId);
            foreach (var group in groups)
            {
                var item = await this.stockItemRepository.GetItem(group.First().ProductId);

                stockItems.AddRange(Enumerable.Repeat(item, group.Count()));
            }

            return stockItems;
        }

        public async Task<CreateBasketResult> CreateBasket(Models.CreateBasketModel model)
        {
            var basket = this.mapper.Map<Data.Models.CreateBasketModel>(model);

            var result = await this.basketRepository.Create(basket);
            
            return new CreateBasketResult
            {
                Success = true,
                BasketId = result.Id
            };
        }

        public async Task AddOffer(string basketId, string offerCode)
        {
            await this.basketRepository.AddOffer(basketId, offerCode);
        }

        public async Task AddGiftCard(string basketId, string voucherCode)
        {
            await this.basketRepository.AddGiftCard(basketId, voucherCode);
        }
    }
}
