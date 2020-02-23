using BasketTest.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Services
{
    public interface IBasketService
    {
        Task<GetBasketModel> GetBasket(string id);

        Task<CreateBasketResult> CreateBasket(CreateBasketModel model);

        Task AddOffer(string id, string offerCode);

        Task AddGiftCard(string basketId, string voucherCode);
    }
}
