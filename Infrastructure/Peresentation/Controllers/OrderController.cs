using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractionLayer;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeresentationLayer.Controllers
{
    [Authorize]
    public class OrderController(IServiceManger _serviceManger) : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var orderToReturn = await _serviceManger.orderServices.CreateOrderAsync(orderDto, GetEmailFromToken());
            return Ok(orderToReturn);
        }

        [HttpPost("Delivery Method")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethod()
        {
            var deliveryMethod = await _serviceManger.orderServices.GetDeliveryMethodAsync();
            return Ok(deliveryMethod);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var orders = await _serviceManger.orderServices.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(orders);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id)
        {
            var order = await _serviceManger.orderServices.GetOrderByIdAsync(id);
            return Ok(order);
        }


    }
}
