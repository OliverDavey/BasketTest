using AutoMapper;
using BasketTest.Data;
using BasketTest.Models;
using BasketTest.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Services
{
    public class BasketValidationService : IBasketValidationService
    {
        private readonly IStockItemRepository stockItemRepository;
        private readonly IMapper mapper;

        public BasketValidationService(
            IStockItemRepository stockItemRepository,
            IMapper mapper)
        {
            this.stockItemRepository = stockItemRepository;
            this.mapper = mapper;
        }

        public async Task<ValidationResult> ValidateCreateBasket(CreateBasketRequest basket)
        {
            if (basket == null)
            {
                return new ValidationResult
                {
                    Success = false,
                    Message = "No basket body supplied"
                };
            }

            foreach (var item in basket.Items)
            {
                if (item.Quantity < 0)
                {
                    return new ValidationResult
                    {
                        Success = false,
                        Message = "Invalid quantity requested"
                    };
                }

                var stockCheck = await this.stockItemRepository.CheckStock(item.ProductId, item.Quantity);
                if (!stockCheck.Success)
                {
                    var response = this.mapper.Map<ValidationResult>(stockCheck);
                    return response;
                }
            }

            return new ValidationResult
            {
                Success = true
            };
        }
    }
}
