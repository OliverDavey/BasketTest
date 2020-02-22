using BasketTest.Models;
using BasketTest.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Services
{
    public interface IBasketValidationService
    {
        Task<ValidationResult> ValidateCreateBasket(CreateBasketRequest basket);
    }
}
