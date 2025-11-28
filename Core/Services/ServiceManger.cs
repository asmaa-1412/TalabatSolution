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
    public class ServiceManger (IUnitOfwork _unitOfwork, IMapper _mapper, IBasketRepository _basketRepository) : IServiceManger
    {
        private readonly Lazy<IProductServices> _LazyproductService = new Lazy<IProductServices>(()=>new ProductServices(_unitOfwork,_mapper));
        public IProductServices productServices =>_LazyproductService.Value;

        private readonly Lazy<IOrderServices> _LazyorderService = new Lazy<IOrderServices>(() => new OrderServices(_unitOfwork, _mapper,_basketRepository));
        public IOrderServices orderServices => _LazyorderService.Value;

        
    }
}
