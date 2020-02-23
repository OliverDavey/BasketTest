using AutoMapper;
using BasketTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Data
{
    public class InMemoryBasketRepository : IBasketRepository
    {
        private IList<Basket> baskets = new List<Basket>();
        private readonly IMapper mapper;

        public InMemoryBasketRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public Task<Basket> Create(CreateBasketModel request)
        {
            var basket = this.mapper.Map<Basket>(request);
            this.baskets.Add(basket);

            return Task.FromResult(basket);
        }

        public Task<Basket> Get(string id)
        {
            var result = this.baskets.FirstOrDefault(basket => basket.Id == id);

            return Task.FromResult(result);
        }

        public async Task<Basket> AddOffer(string basketId, string offerCode)
        {
            var basket = await this.Get(basketId);
            basket.OfferCode = offerCode;

            return basket;
        }

        public async Task<Basket> AddGiftCard(string basketId, string voucherCode)
        {
            var basket = await this.Get(basketId);
            basket.GiftCards.Add(new BasketGiftCard { VoucherCode = voucherCode });
            return basket;
        }
    }
}
