using Floward.Application.Commands.UserCommands;
using Floward.Application.Queries.UserQueries;
using Floward.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Floward.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Result>> Create(CreateUserCommand command)
        {
            try
            {
                return await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                return Result.Failure($"User creation failed. Error: {ex?.Message ?? ex?.InnerException?.Message}");
            }
        }

        [HttpGet("getuserbyid/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Result>> GetUserById(int id)
        {
            try
            {
                return await _mediator.Send(new GetUserByIdQuery { UserId = id });
            }
            catch (Exception ex)
            {
                return Result.Failure($"User retrieval failed. Error: {ex?.Message ?? ex?.InnerException?.Message}");
            }
        }

        [HttpGet("getuserbyuserid/{userid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Result>> GetUserByUserId(string userid)
        {
            try
            {
                return await _mediator.Send(new GetUserByUserIdQuery { UserId = userid });
            }
            catch (Exception ex)
            {
                return Result.Failure($"User retrieval failed. Error: {ex?.Message ?? ex?.InnerException?.Message}");
            }
        }

        [HttpGet("getuserbyemail/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Result>> GetUserByEmail(string email)
        {
            try
            {
                return await _mediator.Send(new GetUserByEmailQuery { Email = email });
            }
            catch (Exception ex)
            {
                return Result.Failure($"User retrieval failed. Error: {ex?.Message ?? ex?.InnerException?.Message}");
            }
        }

        [HttpGet("getallusers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Result>> GetAllUsers()
        {
            try
            {
                return await _mediator.Send(new GetAllUsersQuery());
            }
            catch (Exception ex)
            {
                return Result.Failure($"Users retrieval failed. Error: {ex?.Message ?? ex?.InnerException?.Message}");
            }
        }
    }
}
