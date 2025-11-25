using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServicesAbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer
{
    public class ServiceManger (IUnitOfwork _unitOfwork, IMapper _mapper,
                                IBasketRepository _basketRepository,
                                UserManager<ApplicationUser> userManager,
                                IConfiguration _configration) : IServiceManger
    {
        private readonly Lazy<IProductServices> _LazyproductService = new Lazy<IProductServices>(()=>new ProductServices(_unitOfwork,_mapper));
        public IProductServices ProductServices =>_LazyproductService.Value;

        private readonly Lazy<IBasketServices> _LazyBasketService = new Lazy<IBasketServices>(() => new BasketServices(_basketRepository,_mapper));
        public IBasketServices BasketServices => _LazyBasketService.Value;

        private readonly Lazy<IAuthenticationServices> _LazyAuthenticationService = new Lazy<IAuthenticationServices>(() => new AuthenticationServices(userManager, _configration));
        public IAuthenticationServices AuthenticationServices => _LazyAuthenticationService.Value;
    }
}
