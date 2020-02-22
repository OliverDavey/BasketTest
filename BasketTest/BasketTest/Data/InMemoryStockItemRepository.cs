using BasketTest.Data.Models;
using BasketTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Data
{
    public class InMemoryStockItemRepository : IStockItemRepository
    {
        private readonly IList<StockItem> stockItems;

        public InMemoryStockItemRepository()
        {
            this.stockItems = new List<StockItem>
            {
                new StockItem { Id = "shoddy_hat", Name = "Hat", Price = 10.50m, Quantity = 10, Tags = {ProductTags.HeadGear} },
                new StockItem { Id = "luxury_hat", Name = "Hat", Price = 25.00m, Quantity = 10, Tags = {ProductTags.HeadGear} },
                new StockItem { Id = "thin_jumper", Name = "Jumper", Price = 26.00m, Quantity = 10, Tags = {ProductTags.Winter} },
                new StockItem { Id = "warm_jumper", Name = "Jumper", Price = 54.65m, Quantity = 10, Tags = {ProductTags.Winter} },
                new StockItem { Id = "head_light", Name = "Head Light", Price = 3.50m, Quantity = 10, Tags = {ProductTags.HeadGear, ProductTags.ActiveWear} }
            };
        }

        public Task<CheckStockResult> CheckStock(string itemId, int quantity)
        {
            var stockItem = this.stockItems.FirstOrDefault(item => item.Id == itemId);

            if (stockItem == null)
            {
                return Task.FromResult(new CheckStockResult { Success = false, Message = "Item doesn't exist" });
            }

            if (stockItem.Quantity < quantity)
            {
                return Task.FromResult(new CheckStockResult { Success = false, Message = "Stock too low" });
            }

            return Task.FromResult(new CheckStockResult { Success = true });
        }
    }
}
