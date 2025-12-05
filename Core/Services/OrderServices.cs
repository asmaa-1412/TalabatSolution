using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModel;
using DomainLayer.Models.OrderModels;
using DomainLayer.Models.ProductModels;
using Microsoft.VisualBasic;
using ServicesAbstractionLayer;
using ServicesLayer.Specifications;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer
{
    public class OrderServices(IUnitOfwork _unitOfwork, IMapper _mapper, IBasketRepository _basketRepository) : IOrderServices
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email)
        {
            var orderAddress = _mapper.Map<OrderAddress>(orderDto.ShipToAddress);
            var basket = await _basketRepository.GetBasketAsync(orderDto.BasketId)
                ?? throw new BasketNotFoundException(orderDto.BasketId);

            List<OrderItem> orderItems = [];

            foreach (var basketItem in basket.Items)
            {
                var originalProduct = await _unitOfwork.GetRepository<Product, int>().GetByIdAsync(basketItem.Id)
                    ?? throw new ProductNotFoundException(basketItem.Id);

                var orderItem = new OrderItem()
                {
                    Product = new ProductItemOrdered()
                    {
                        ProductId = originalProduct.Id,
                        PictureUrl = originalProduct.PictureUrl,
                        ProductName = originalProduct.Name,
                    },
                        Price = originalProduct.Price,
                        Quantity = basketItem.Quantity
                };

                orderItems.Add(orderItem);
            }

            var deliveryMethod = await _unitOfwork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId)
                                 ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);

            var subTotal = orderItems.Sum(i => i.Quantity * i.Price);
            var order = new Order(email, orderAddress, deliveryMethod, orderItems, subTotal);
            await _unitOfwork.GetRepository<Order, Guid>().AddAsync(order);
            await _unitOfwork.SaveChangesAsync();
            return _mapper.Map<OrderToReturnDto>(order);
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email)
        {
            var specs = new OrderSpecifications(email);
            var orders = await _unitOfwork.GetRepository<Order, Guid>().GetAllAsync(specs);
            return _mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
        {
            var deliveryMethods = await _unitOfwork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethods);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid id)
        {
            var specs = new OrderSpecifications(id);
            var order = await _unitOfwork.GetRepository<Order, Guid>().GetByIdAsync(specs);
            return _mapper.Map<OrderToReturnDto>(order);
        }
    }
}
