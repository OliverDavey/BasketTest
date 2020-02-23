using BasketTest.SDK.Models;
using BasketTest.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Services
{
    public interface IBasketValidationService
    {
        Task<ValidationResult> ValidateCreateBasket(CreateBasketRequest basket);

        Task<ValidationResult> CheckOffer(GetBasketModel basket, string offerCode);

        Task<ValidationResult> CheckGiftCard(GetBasketModel basket, string voucherCode);
    }
}
