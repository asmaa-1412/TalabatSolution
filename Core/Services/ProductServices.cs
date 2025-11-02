using DomainLayer.Contracts;
using DomainLayer.Models;
using ServicesAbstractionLayer;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer
{
    public class ProductServices(IUnitOfwork _unitOfwork) : IProductServices
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            throw new NotImplementedException();

        }

        public Task<IEnumerable<ProductDto>> GetAllProductAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> GetProductByIdAsync(int ind)
        {
            throw new NotImplementedException();
        }
    }
}
