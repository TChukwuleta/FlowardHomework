using Floward.Domain.Entities;
using Floward.Domain.Enums;
using Floward.Domain.Interfaces.IRepositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floward.Webapi.Tests.Mocks
{
    internal class MockIProductRepository
    {
        public static Mock<IProductRepository> GetMock()
        {
            var mock = new Mock<IProductRepository>();
            var products = new List<Product>()
            {

                new Product()
                {
                    Id = 2,
                    ProductImage = "testimage.jpeg",
                    Cost = 1000,
                    Discount = 100,
                    ProductType = ProductType.Groceries,
                    Price = 900,
                    CreatedDate = DateTime.Now,
                    Status = Status.Active,
                    StatusDesc = Status.Active.ToString()
                }
            };
            mock.Setup(c => c.GetAllAsync()).Returns(() => products);
            mock.Setup(c => c.GetByIdAsync(It.IsAny<int>())).Returns((int id) => products.FirstOrDefault(c => c.Id == id));
            mock.Setup(c => c.AddAsync(It.IsAny<Product>())).Callback(() => { return; });
            mock.Setup(c => c.UpdateAsync(It.IsAny<Product>())).Callback(() => { return; });
            mock.Setup(c => c.DeleteAsync(It.IsAny<Product>())).Callback(() => { return; });
            return mock;
        }
    }
}
