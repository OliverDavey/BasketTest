using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketTest.SDK.Models;
using BasketTest.Models;
using BasketTest.Data.Models;

namespace BasketTest.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            // Creation
            CreateMap<SDK.Models.CreateBasketRequest, Models.CreateBasketModel>();
            CreateMap<SDK.Models.CreateBasketItem, Models.CreateBasketItem>();

            CreateMap<Models.CreateBasketModel, Data.Models.Basket>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid().ToString()));
            CreateMap<Models.CreateBasketItem, Data.Models.BasketItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid().ToString()));

            // Reading
            CreateMap<Data.Models.Basket, Services.Models.GetBasketModel>();
            CreateMap<Data.Models.BasketItem, Services.Models.GetBasketItemModel>();
            CreateMap<Services.Models.GetBasketModel, SDK.Models.RetrieveBasketResponse>();
            CreateMap<Services.Models.GetBasketItemModel, SDK.Models.RetrieveBasketItem>(); //perhaps won't be needed when we aggregate them...

            CreateMap<CheckStockResult, ValidationResult>();
        }
    }
}
