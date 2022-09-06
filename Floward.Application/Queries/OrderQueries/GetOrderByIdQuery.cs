using Floward.Domain.Interfaces.IRepositories;
using Floward.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floward.Application.Queries.OrderQueries
{
    public class GetOrderByIdQuery : IRequest<Result>
    {
        public int OrderId { get; set; }
    }

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var existingOrder = await _orderRepository.GetByIdAsync(request.OrderId);
                return Result.Success("Order retrieval was successful", existingOrder);
            }
            catch (Exception ex)
            {
                return Result.Failure(new string[] { "Retrieving order by Id was not successful", ex?.Message ?? ex?.InnerException.Message });
            }
        }
    }
}
