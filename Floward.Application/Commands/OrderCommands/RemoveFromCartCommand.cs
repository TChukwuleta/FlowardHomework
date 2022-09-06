using Floward.Domain.Interfaces.IRepositories;
using Floward.Domain.Model;
using Floward.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floward.Application.Commands.OrderCommands
{
    public class RemoveFromCartCommand : IRequest<Result>
    {
        public int OrderId { get; set; }
    }

    public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ApplicationDbContext _context;
        public RemoveFromCartCommandHandler(IOrderRepository orderRepository, ApplicationDbContext context)
        {
            _orderRepository = orderRepository;
            _context = context;
        }

        public async Task<Result> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                /*var existingOrder = await _context.Orders
                    .FirstOrDefaultAsync(c => c.UserId == request.UserId && c.Id == request.OrderId);
                if (existingOrder == null)
                {
                    return Result.Failure("Order does not exist");
                }*/

                var existingOrder = await _orderRepository.GetByIdAsync(request.OrderId);
                await _orderRepository.DeleteAsync(existingOrder);
                return Result.Success("Order was successfully removed from Cart");
            }
            catch (Exception ex)
            {
                return Result.Failure(new string[] { "Removing order from cart was not successful", ex?.Message ?? ex?.InnerException.Message });
            }
        }
    }
}
