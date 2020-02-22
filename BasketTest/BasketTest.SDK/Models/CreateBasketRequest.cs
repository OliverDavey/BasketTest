using System;
using System.Collections.Generic;

namespace BasketTest.SDK.Models
{
    public class CreateBasketRequest
    {
        public IList<CreateBasketItem> Items { get; set; } = new List<CreateBasketItem>();
    }
}
