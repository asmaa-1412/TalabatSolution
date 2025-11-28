using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstractionLayer
{
    public interface IOrderServices
    {
        public Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email);
    }
}
