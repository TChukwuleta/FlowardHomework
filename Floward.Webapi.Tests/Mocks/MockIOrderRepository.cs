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
    internal class MockIOrderRepository
    {
        public static Mock<IOrderRepository> GetMock()
        {
            var mock = new Mock<IOrderRepository>();
            var orders = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    CreatedDate = DateTime.Now,
                    UserId = Guid.NewGuid().ToString(),
                    Quantity = 5,
                    OrderStatus = OrderStatus.OrderProcessing,
                    Status = Status.Active,
                    StatusDesc = Status.Active.ToString(),
                    Products = new List<Product>()
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
                    }
                }
            };
            mock.Setup(c => c.GetAllAsync()).Returns(() => orders);
            mock.Setup(c => c.GetByIdAsync(It.IsAny<int>())).Returns((int id) => orders.FirstOrDefault(c => c.Id == id));
            mock.Setup(c => c.AddAsync(It.IsAny<Order>())).Callback(() => { return; });
            mock.Setup(c => c.UpdateAsync(It.IsAny<Order>())).Callback(() => { return; });
            mock.Setup(c => c.DeleteAsync(It.IsAny<Order>())).Callback(() => { return; });
            return mock;
        }
    }
}
