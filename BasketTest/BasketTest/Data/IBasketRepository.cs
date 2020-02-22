﻿using BasketTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Data
{
    public interface IBasketRepository
    {

        Task<Basket> Get(string id);
    }
}
