using Floward.Application.Commands.ProductCommands;
using Floward.Application.Queries.ProductQueries;
using Floward.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Floward.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Result>> CreateProduct(CreateProductCommand command)
        {
            try
            {
                return await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                return Result.Failure($"Product creation failed. Error: {ex?.Message ?? ex?.InnerException?.Message}");
            }
        }

        [HttpPost("changeproductstatus")]
        public async Task<ActionResult<Result>> ChangeProductStatus(ChangeProductStatusCommand command)
        {
            try
            {
                return await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                return Result.Failure($"Product status update failed. Error: {ex?.Message ?? ex?.InnerException?.Message}");
            }
        }

        [HttpGet("getproductbyid/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Result>> GetProductById(int id)
        {
            try
            {
                return await _mediator.Send(new GetProductByIdQuery { ProductId = id });
            }
            catch (Exception ex)
            {
                return Result.Failure($"Product retrieval failed. Error: {ex?.Message ?? ex?.InnerException?.Message}");
            }
        }

        [HttpGet("getallproducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Result>> GetAllProducts()
        {
            try
            {
                return await _mediator.Send(new GetAllProductsQuery());
            }
            catch (Exception ex)
            {
                return Result.Failure($"Products retrieval failed. Error: {ex?.Message ?? ex?.InnerException?.Message}");
            }
        }
    }
}
