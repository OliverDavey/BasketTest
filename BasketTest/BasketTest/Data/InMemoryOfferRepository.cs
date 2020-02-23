using BasketTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Data
{
    public class InMemoryOfferRepository : IOfferRepository
    {
        private readonly IList<Offer> offers;

        public InMemoryOfferRepository()
        {
            this.offers = new List<Offer>
            {
                new Offer { Code = "headgear_5_50", Value = 5.00m, Threshold = 50.01m, ApplicableItems = { ProductTags.HeadGear } },
                new Offer { Code = "anything_5_50", Value = 5.00m, Threshold = 50.01m }
            };
        }

        public Task<Offer> GetOffer(string offerCode)
        {
            var offer = this.offers.FirstOrDefault(o => o.Code == offerCode);

            return Task.FromResult(offer);
        }
    }
}
