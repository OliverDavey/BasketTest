using BasketTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Data
{
    public interface IStockItemRepository
    {
        Task<CheckStockResult> CheckStock(string itemId, int quantity);
    }
}
