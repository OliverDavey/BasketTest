using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketTest.SDK.Models;

namespace BasketTest.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<GetBasketResponse, Models.Basket>();
        }
    }
}
