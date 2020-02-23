using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Data.Models
{
    public class Basket
    {
        public string Id { get; set; }

        public IEnumerable<BasketItem> Items { get; set; }

        public string OfferCode { get; set; }
    }
}
