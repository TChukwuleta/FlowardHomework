using Floward.Domain.Entities;
using Floward.Domain.Interfaces.IRepositories;
using Floward.Infrastructure.Data;
using Floward.Infrastructure.Services.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floward.Infrastructure.Services.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
