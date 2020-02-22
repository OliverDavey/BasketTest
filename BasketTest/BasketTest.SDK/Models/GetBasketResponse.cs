using System;
using System.Collections.Generic;

namespace BasketTest.SDK.Models
{
    public class GetBasketResponse
    {
        public string Id { get; set; }

        public IList<GetBasketItem> Items { get; set; } = new List<GetBasketItem>();
    }
}
