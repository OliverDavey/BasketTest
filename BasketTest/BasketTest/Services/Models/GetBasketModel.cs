using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Services.Models
{
    public  class GetBasketModel
    {
        public string Id { get; set; }

        public IList<GetBasketItemModel> Items { get; set; } = new List<GetBasketItemModel>();

        public IList<GetBasketGiftCardModel> RedeemedCards { get; set; } = new List<GetBasketGiftCardModel>();

        public decimal SubTotal { get; set; }

        public decimal DiscountableTotal { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
