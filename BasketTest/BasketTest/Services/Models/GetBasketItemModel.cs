using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Services.Models
{
    public class GetBasketItemModel
    {
        public string ProductId { get; set; }

        public string ItemId { get; set; }

        public decimal Price { get; set; }
    }
}
