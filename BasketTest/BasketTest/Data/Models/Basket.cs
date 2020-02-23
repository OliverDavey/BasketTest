using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Data.Models
{
    public class Basket
    {
        public string Id { get; set; }

        public IList<BasketItem> Items { get; set; } = new List<BasketItem>();

        public IList<BasketGiftCard> GiftCards { get; set; } = new List<BasketGiftCard>();

        public string OfferCode { get; set; }
    }
}
