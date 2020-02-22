using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketTest.SDK.Models;
using BasketTest.Models;

namespace BasketTest.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<Models.Basket, GetBasketResponse>();
            CreateMap<CheckStockResult, ValidationResult>();
        }
    }
}
