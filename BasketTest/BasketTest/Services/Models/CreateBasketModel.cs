using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Services.Models
{
    public class CreateBasketModel
    {
        public IList<PricedBasketItemModel> Items { get; set; } = new List<PricedBasketItemModel>();
    }
}
