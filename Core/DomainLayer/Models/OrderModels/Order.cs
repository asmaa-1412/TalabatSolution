using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModels
{
    public class Order:BaseEntity<Guid>
    {
        public string UserEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderAddress Address { get; set; } = null!;
        public DeliveryMethod DeliveryMethod { get; set; } = null!;
        public int DeliveryMethodId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Collection<OrderItem> Items { get; set; } = [];
        public decimal SubTotal { get; set; }
        [NotMapped]
        public decimal Total { get => SubTotal + DeliveryMethod.Price; }
}
}
