using AutoMapper;
using BasketTest.Data;
using BasketTest.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Services
{
    public class PricingService : IPricingService
    {
        private readonly IStockItemRepository stockItemRepository;
        private readonly IMapper mapper;

        public PricingService(
            IStockItemRepository stockItemRepository,
            IMapper mapper)
        {
            this.stockItemRepository = stockItemRepository;
            this.mapper = mapper;
        }

        public async Task<IList<PricedBasketItemModel>> PriceItems(IList<UnpricedBasketItemModel> items)
        {
            var groups = items.GroupBy(item => item.ProductId);

            var pricedItems = new List<PricedBasketItemModel>();

            foreach (var group in groups)
            {
                var item = await this.stockItemRepository.GetItem(group.First().ProductId);
                var pricedItem = mapper.Map<PricedBasketItemModel>(item);

                var totalQuantity = group.Sum(item => item.Quantity);
                pricedItems.AddRange(Enumerable.Repeat(pricedItem, totalQuantity));
            }

            return pricedItems;
        }
    }
}
