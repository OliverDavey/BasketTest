using BasketTest.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Services
{
    public interface IPricingService
    {
        Task<IList<PricedBasketItemModel>> PriceItems(IList<UnpricedBasketItemModel> items);
    }
}
