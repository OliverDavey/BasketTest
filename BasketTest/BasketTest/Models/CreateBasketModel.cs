using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Models
{
    public class CreateBasketModel
    {
        public IEnumerable<CreateBasketItem> Items { get; set; }
    }
}
