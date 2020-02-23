using BasketTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Data
{
    public class InMemoryGiftCardRepository : IGiftCardRepository
    {
        private readonly IList<GiftCard> giftCards;

        public InMemoryGiftCardRepository()
        {
            this.giftCards = new List<GiftCard>
            {
                new GiftCard { VoucherCode = "XXX-XXX", Value = 5.00m }
            };
        }

        public Task<GiftCard> GetGiftCard(string voucherCode)
        {
            var giftCard = this.giftCards.FirstOrDefault(card => card.VoucherCode == voucherCode);

            return Task.FromResult(giftCard);
        }
    }
}
