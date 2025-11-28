using AutoMapper;
using DomainLayer.Models.OrderModels;
using Shared.Dtos.IdentityDto;
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
        }
    }
}
