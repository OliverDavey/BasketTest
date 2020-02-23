using BasketTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Data
{
    public interface IBasketRepository
    {
        Task<Basket> Create(CreateBasketModel request);

        Task<Basket> Get(string id);

        Task<Basket> AddOffer(string basketId, string offerCode);
    }
}
