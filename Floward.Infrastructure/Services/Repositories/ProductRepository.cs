using Floward.Domain.Entities;
using Floward.Domain.Interfaces.IRepositories;
using Floward.Infrastructure.Data;
using Floward.Infrastructure.Services.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floward.Infrastructure.Services.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Product> GetProductByName(string name)
        {
            try
            {
                var existingProduct = await _context.Products.FirstOrDefaultAsync(c => c.Name == name);
                return existingProduct;
            }
            catch (Exception ex)
            { 

                throw ex;
            }
        }
    }
}
