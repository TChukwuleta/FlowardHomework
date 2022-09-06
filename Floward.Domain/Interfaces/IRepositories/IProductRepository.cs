using Floward.Domain.Entities;
using Floward.Domain.Interfaces.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floward.Domain.Interfaces.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductByName(string name);
    }
}
