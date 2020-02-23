using System;
using System.Collections.Generic;

namespace BasketTest.SDK.Models
{
    public class RetrieveBasketResponse
    {
        public string Id { get; set; }

        public IList<RetrieveBasketItem> Items { get; set; } = new List<RetrieveBasketItem>();

        public decimal SubTotal { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
