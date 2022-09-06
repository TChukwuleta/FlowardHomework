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
    public class GetProductByIdQuery : IRequest<Result>
    {
        public int ProductId { get; set; }
    }
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var existingProduct = await _productRepository.GetByIdAsync(request.ProductId);
                return Result.Success("Product retrieval was successful", existingProduct);
            }
            catch (Exception ex)
            {
                return Result.Failure(new string[] { "Retrieving product by Id was not successful", ex?.Message ?? ex?.InnerException.Message });
            }
        }
    }
}
