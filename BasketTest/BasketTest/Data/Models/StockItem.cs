﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Data.Models
{
    public class StockItem
    {
        public string ProductId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public IList<ProductTags> Tags { get; set; } = new List<ProductTags>();
    }
}
