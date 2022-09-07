using Floward.Domain.Entities;
using Floward.Domain.Enums;
using Floward.Domain.Interfaces.IRepositories;
using Floward.Domain.Interfaces.MessageBroker;
using Floward.Domain.Model;
using Floward.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floward.Application.Commands.OrderCommands
{
    public class AddToCartCommand : IRequest<Result>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
    }

    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;
        private readonly IBrokerService _brokerService;
        public AddToCartCommandHandler(IOrderRepository orderRepository, ApplicationDbContext context, IUserRepository userRepository, IBrokerService brokerService)
        {
            _orderRepository = orderRepository;
            _brokerService = brokerService;
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByUserId(request.UserId);
                if (existingUser == null)
                {
                    return Result.Failure("Invalid user");
                }
                var existingOrder = await _context.Orders.Include(c => c.Products)
                    .Where(c => c.Products.Any(t => t.Id == request.ProductId && t.Status == Status.Active))
                    .FirstOrDefaultAsync(c => c.UserId == request.UserId);
                if (existingOrder != null)
                {
                    existingOrder.Quantity = existingOrder.Quantity + request.Quantity;
                    existingOrder.LastModifiedDate = DateTime.Now;
                    await _orderRepository.UpdateAsync(existingOrder);
                    return Result.Success("Order was successfully added to Cart");
                }
                var existingProduct = await _context.Products.FirstOrDefaultAsync(c => c.Id == request.ProductId);
                if(existingOrder == null)
                {
                    return Result.Failure("Invalid product selected");
                }
                var newOrder = new Order
                {
                    Status = Status.Active,
                    StatusDesc = Status.Active.ToString(),
                    CreatedDate = DateTime.Now,
                    UserId = request.UserId,
                    Quantity = request.Quantity
                };
                List<Product> products = new List<Product>();
                products.Add(existingProduct);
                newOrder.Products = products;
                var result = await _orderRepository.AddAsync(newOrder);
                var integrationEventData = JsonConvert.SerializeObject(new
                {
                    Id = result.Id,
                    name = result.Quantity
                });
                _brokerService.PublishToMessageQueue("user.orderservice", integrationEventData);
                return Result.Success("Order was successfully added to Cart");
            }
            catch (Exception ex)
            {
                return Result.Failure(new string[] { "Adding order to cart was not successful", ex?.Message ?? ex?.InnerException.Message });
            }
        }
    }
}
