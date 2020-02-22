﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Services.Models
{
    public  class GetBasketModel
    {
        public string Id { get; set; }

        public IList<GetBasketItemModel> Items { get; set; } = new List<GetBasketItemModel>();

        public decimal SubTotal { get; set; } //Maybe this might be worth displaying?

        public decimal TotalPrice { get; set; }
    }
}