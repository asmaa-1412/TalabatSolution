using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServicesAbstractionLayer;
using ServicesLayer.Specifications;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer
{
    public class ProductServices(IUnitOfwork _unitOfwork,IMapper _mapper) : IProductServices
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfwork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDto>>(brands);

        }

        public async Task<IEnumerable<ProductDto>> GetAllProductAsync(int? brandId, int? typeId)
        {
            var spec = new ProductWithBrandandTypeSpecifications(brandId,typeId);
            var products = await _unitOfwork.GetRepository<Product, int>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var types = await _unitOfwork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDto>>(types);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandandTypeSpecifications(id);
            var product = await _unitOfwork.GetRepository<Product, int>().GetByIdAsync(spec);
            return _mapper.Map<ProductDto>(product);
        }
    }
}
