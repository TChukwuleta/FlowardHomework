using Floward.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floward.Domain.Entities
{
    public class Product : AuditableEntity
    {
        public string Name { get; set; }
        public string ProductImage { get; set; }
        public decimal Cost { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public ProductType ProductType { get; set; }
    }
}
