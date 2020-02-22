using BasketTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Data
{
    public class InMemoryBasketRepository : IBasketRepository
    {
        private IList<Basket> baskets = new List<Basket>();

        //public Task<Basket> Create

        public Task<Basket> Get(string id)
        {
            var result = this.baskets.FirstOrDefault(basket => basket.Id == id);

            return Task.FromResult(result);
        }
    }
}
