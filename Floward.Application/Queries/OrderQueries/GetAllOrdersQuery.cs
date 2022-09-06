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
    public class GetAllOrdersQuery : IRequest<Result>
    {
    }

    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, Result>
    {
        private readonly IOrderRepository _orderRepository;
        public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var allOrders = await _orderRepository.GetAllAsync();
                return Result.Success("Orders retrieval was successful", allOrders);
            }
            catch (Exception ex)
            {
                return Result.Failure(new string[] { "Retrieving all available products was not successful", ex?.Message ?? ex?.InnerException.Message });
            }
        }
    }
}
