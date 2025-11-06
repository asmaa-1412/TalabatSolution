using DomainLayer.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Specifications
{
    public class ProductWithBrandandTypeSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandandTypeSpecifications(int? brandId, int? typeId, ProductSortingOptions sortingOption)
            : base(p => (!brandId.HasValue || p.BrandId==brandId) && (!typeId.HasValue || p.TypeId == typeId))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (sortingOption)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderby(p => p.Name);
                    break;

                case ProductSortingOptions.NameDesc:
                    AddOrderbyDesc(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderby(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderbyDesc(p => p.Price);
                    break;
                default:
                    break;
            }
        }

        public ProductWithBrandandTypeSpecifications(int id) : base(p=>p.Id==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
