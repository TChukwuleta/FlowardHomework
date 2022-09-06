using Floward.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floward.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public string UserId { get; set; }
        public int Quantity { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
