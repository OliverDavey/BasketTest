using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Data.Models
{
    public class Offer
    {
        public string Code { get; set; }

        public decimal Value { get; set; }

        public IList<ProductTags> ApplicableItems { get; set; } = new List<ProductTags>();

        public decimal Threshold { get; set; }
    }
}
