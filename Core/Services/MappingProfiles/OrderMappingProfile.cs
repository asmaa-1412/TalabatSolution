using AutoMapper;
using DomainLayer.Models.OrderModels;
using Shared.Dtos.IdentityDto;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.MappingProfiles
{
    public class OrderMappingProfile:Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<AddressDto, OrderAddress>().ReverseMap();

            CreateMap<Order,OrderToReturnDto>()
                .ForMember(dest=>dest.DeliveryMethod,opt=>opt
                .MapFrom(src=>src.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
               .ForMember(dest => dest.ProductName, opt => opt
               .MapFrom(src => src.Product.ProductName))
               .ForMember(dest => dest.PictureUrl, opt => opt
               .MapFrom<OrderItemPictureUrlResolver>());

        }
    }
}
