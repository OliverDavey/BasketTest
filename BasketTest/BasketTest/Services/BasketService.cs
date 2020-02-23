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
        private readonly IMapper mapper;

        public BasketService(
            IBasketRepository basketRepository,
            IStockItemRepository stockItemRepository,
            IOfferRepository offerRepository,
            IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.stockItemRepository = stockItemRepository;
            this.offerRepository = offerRepository;
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

            var stockItems = new List<StockItem>();
            var groups = basket.Items.GroupBy(item => item.ProductId);
            foreach (var group in groups)
            {
                var item = await this.stockItemRepository.GetItem(group.First().ProductId);

                stockItems.AddRange(Enumerable.Repeat(item, group.Count()));
            }

            result.SubTotal = stockItems.Sum(item => item.Price);

            // This should be handled elsewhere
            var nonDiscountableTags = new List<ProductTags> { ProductTags.GiftCard };
            result.DiscountableTotal = stockItems
                .Where(item => !item.Tags.Intersect(nonDiscountableTags).Any())
                .Sum(item => item.Price);

            // Offers are validated at point of basket changing. We can trust that they will still apply here
            var offerDiscount = basket.OfferCode != null
                ? (await this.offerRepository.GetOffer(basket.OfferCode)).Value
                : 0m;

            result.TotalDiscount = Math.Min(offerDiscount, result.SubTotal);

            //result.DiscountedTotal = min(totalofgiftcards + offer, result.SubTotal);

            result.TotalPrice = result.SubTotal - result.TotalDiscount; // Maybe computed property?

            return result;
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

        public async Task AddOffer(string id, string offerCode) // Should probably accept and return a model
        {
            await this.basketRepository.AddOffer(id, offerCode);
        }
    }
}
