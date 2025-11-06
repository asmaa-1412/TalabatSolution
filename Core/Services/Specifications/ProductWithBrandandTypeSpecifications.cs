using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Specifications
{
    public class ProductWithBrandandTypeSpecifications:BaseSpecifications<Product,int>
    {
        public ProductWithBrandandTypeSpecifications():base(null)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }

        public ProductWithBrandandTypeSpecifications(int id) : base(p=>p.Id==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
