using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Models
{
    public class StockItem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public IList<ProductTags> Tags { get; set; } = new List<ProductTags>();
    }
}
