using AutoMapper;
using BasketTest.Data;
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
        private readonly IMapper mapper;

        public BasketService(
            IBasketRepository basketRepository,
            IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }

        public async Task<GetBasketModel> GetBasket(string id)
        {
            var basket = await this.basketRepository.Get(id);

            var result = this.mapper.Map<GetBasketModel>(basket);
            result.SubTotal = basket.Items.Sum(item => item.Price);
            result.TotalPrice = basket.Items.Sum(item => item.Price);

            // going to need to know about tags here
            //result.DiscountableTotal = basket.Items.Where(item => item)

            return result;
        }

        public async Task<CreateBasketResult> CreateBasket(CreateBasketModel model)
        {
            var basket = this.mapper.Map<Data.Models.CreateBasketModel>(model);

            var result = await this.basketRepository.Create(basket);
            
            return new CreateBasketResult
            {
                Success = true,
                BasketId = result.Id
            };
        }
    }
}
