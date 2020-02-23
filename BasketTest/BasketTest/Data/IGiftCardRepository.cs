﻿using BasketTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Data
{
    public interface IGiftCardRepository
    {
        Task<GiftCard> GetGiftCard(string voucherCode);
    }
}
