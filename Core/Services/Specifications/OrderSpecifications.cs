using DomainLayer.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Specifications
{
    public class OrderSpecifications:BaseSpecifications<Order,Guid>
    {
        public OrderSpecifications(string email) : base(O=>O.UserEmail == email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            AddOrderbyDesc(O => O.OrderDate);
        }
        public OrderSpecifications(Guid id): base(O => O.Id == id)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
        }
    }
}
