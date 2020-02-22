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
            // Creation
            CreateMap<SDK.Models.CreateBasketRequest, Models.CreateBasketModel>();
            CreateMap<SDK.Models.CreateBasketItem, Models.CreateBasketItem>();

            CreateMap<Models.CreateBasketModel, Models.Basket>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid().ToString()));
            CreateMap<Models.CreateBasketItem, Models.BasketItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid().ToString()));

            // Reading
            CreateMap<Models.Basket, SDK.Models.GetBasketResponse>();
            CreateMap<Models.BasketItem, SDK.Models.GetBasketItem>(); //perhaps won't be needed when we aggregate them...

            CreateMap<CheckStockResult, ValidationResult>();
        }
    }
}
