using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketTest.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            // Creation
            CreateMap<SDK.Models.CreateBasketRequest, Services.Models.CreateBasketModel>();
            CreateMap<SDK.Models.CreateBasketItem, Services.Models.UnpricedBasketItemModel>();
            CreateMap<SDK.Models.CreateBasketItem, Services.Models.PricedBasketItemModel>();

            CreateMap<Services.Models.CreateBasketModel, Data.Models.CreateBasketModel>();
            CreateMap<Data.Models.CreateBasketModel, Data.Models.Basket>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid().ToString()));
            CreateMap<Services.Models.PricedBasketItemModel, Data.Models.CreateBasketItem>();
            CreateMap<Data.Models.CreateBasketItem, Data.Models.BasketItem>()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(_ => Guid.NewGuid().ToString()));

            // Reading
            CreateMap<Data.Models.Basket, Services.Models.GetBasketModel>();
            CreateMap<Data.Models.BasketItem, Services.Models.GetBasketItemModel>();
            CreateMap<Data.Models.GiftCard, Services.Models.GetBasketGiftCardModel>();
            CreateMap<Services.Models.GetBasketModel, SDK.Models.RetrieveBasketResponse>();
            CreateMap<Services.Models.GetBasketItemModel, SDK.Models.RetrieveBasketItem>();

            CreateMap<Data.Models.StockItem, Services.Models.PricedBasketItemModel>();

            CreateMap<Data.Models.CheckStockResult, Services.Models.ValidationResult>();
        }
    }
}
