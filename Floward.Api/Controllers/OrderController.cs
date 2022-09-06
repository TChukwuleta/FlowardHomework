using Floward.Application.Commands.OrderCommands;
using Floward.Application.Queries.OrderQueries;
using Floward.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Floward.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("addtocart")]
        public async Task<ActionResult<Result>> AddToCart(AddToCartCommand command)
        {
            try
            {
                return await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                return Result.Failure($"Order adding to cart failed. Error: {ex?.Message ?? ex?.InnerException?.Message}");
            }
        }

        [HttpPost("removefromcart")]
        public async Task<ActionResult<Result>> RemoveFromCart(RemoveFromCartCommand command)
        {
            try
            {
                return await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                return Result.Failure($"Remove order from cart failed. Error: {ex?.Message ?? ex?.InnerException?.Message}");
            }
        }

        [HttpGet("getorderbyid/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Result>> GetOrderById(int id)
        {
            try
            {
                return await _mediator.Send(new GetOrderByIdQuery { OrderId = id });
            }
            catch (Exception ex)
            {
                return Result.Failure($"Order retrieval failed. Error: {ex?.Message ?? ex?.InnerException?.Message}");
            }
        }

        [HttpGet("getallorders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Result>> GetAllOrders()
        {
            try
            {
                return await _mediator.Send(new GetAllOrdersQuery());
            }
            catch (Exception ex)
            {
                return Result.Failure($"Orders retrieval failed. Error: {ex?.Message ?? ex?.InnerException?.Message}");
            }
        }
    }
}
