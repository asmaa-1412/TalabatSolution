using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractionLayer;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeresentationLayer
{
    public class OrderController(IServiceManger _serviceManger): ApiBaseController
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var orderToReturn = await _serviceManger.orderServices.CreateOrderAsync(orderDto, GetEmailFromToken());
            return Ok(orderToReturn);
        }

    }
}
