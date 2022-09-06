using Floward.Domain.Enums;
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

namespace Floward.Application.Commands.ProductCommands
{
    public class ChangeProductStatusCommand : IRequest<Result>
    {
        public int ProductId { get; set; }
    }

    public class ChangeProductStatusCommandHandler : IRequestHandler<ChangeProductStatusCommand, Result>
    {
        private readonly ApplicationDbContext _context;
        private readonly IOrderRepository _orderRepository;
        public ChangeProductStatusCommandHandler(ApplicationDbContext context, IOrderRepository orderRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
        }

        public async Task<Result> Handle(ChangeProductStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string message = "";
                var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == request.ProductId);
                if (product == null)
                {
                    return Result.Failure(new string[] { "Product  is invalid" });
                }

                switch (product.Status)
                {
                    case Status.Active:
                        product.Status = Status.Inactive;
                        message = "product status was changed to inactive successfully";
                        break;
                    case Status.Inactive:
                        product.Status = Status.Active;
                        message = "Currency  status was changed to active successfully";
                        break;
                    case Status.Deactivated:
                        product.Status = Status.Active;
                        message = "Currency  status was changed to active successfully";
                        break;
                    default:
                        break;
                }
                _context.Products.Update(product);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success(message);
            }
            catch (Exception ex)
            {
                return Result.Failure(new string[] { "Product status change was not successful", ex?.Message ?? ex?.InnerException.Message });
            }
        }
    }
}
