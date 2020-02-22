using BasketTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Data
{
    public interface IStockItemRepository
    {
        Task<CheckStockResult> CheckStock(string productId, int quantity);

        Task<StockItem> GetItem(string itemId);
    }
}
