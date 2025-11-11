using AutoMapper;
using DomainLayer.Models.ProductModels;
using Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.MappingProfiles
{
    public class ProductMappingProfile:Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest=>dest.BrandName,option=>option.MapFrom(src=>src.ProductBrand.Name))
                .ForMember(dest => dest.TypeName, option => option.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, option => option.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }
    }
}
