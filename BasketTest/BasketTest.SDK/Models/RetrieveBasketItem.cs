using System;
using System.Collections.Generic;
using System.Text;

namespace BasketTest.SDK.Models
{
    public class RetrieveBasketItem
    {
        public string ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
