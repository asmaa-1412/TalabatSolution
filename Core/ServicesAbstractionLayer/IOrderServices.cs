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
        Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email);
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync();
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email);
        Task<OrderToReturnDto> GetOrderByIdAsync(Guid id);


    }
}
