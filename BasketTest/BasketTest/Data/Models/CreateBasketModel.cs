﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Data.Models
{
    public class CreateBasketModel
    {
        public IList<CreateBasketItem> Items { get; set; } = new List<CreateBasketItem>();
    }
}
