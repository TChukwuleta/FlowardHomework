using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floward.Domain.Entities
{
    public class ApplicationUser : AuditableEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
    }
}
