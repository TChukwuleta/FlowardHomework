using Floward.Domain.Interfaces.IRepositories;
using Floward.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floward.Application.Queries.ProductQueries
{
    public class GetAllProductsQuery : IRequest<Result>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var allProducts = await _productRepository.GetAllAsync();
                return Result.Success("Products retrieval was successful", allProducts);
            }
            catch (Exception ex)
            {
                return Result.Failure(new string[] { "Retrieving all available products was not successful", ex?.Message ?? ex?.InnerException.Message });
            }
        }
    }
}
