using AutoMapper;
using DomainLayer.Contracts;
using ServicesAbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer
{
    public class ServiceManger (IUnitOfwork _unitOfwork, IMapper _mapper) : IServiceManger
    {
        private readonly Lazy<IProductServices> _LazyproductService = new Lazy<IProductServices>(()=>new ProductServices(_unitOfwork,_mapper));
        public IProductServices productServices =>_LazyproductService.Value;
    }
}
