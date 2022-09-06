using Floward.Domain.Interfaces.IRepositories;
using Floward.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floward.Application.Queries.UserQueries
{
    public class GetUserByUserIdQuery : IRequest<Result>
    {
        public string UserId { get; set; }
    }

    public class GetUserByUserIdQueryHandler : IRequestHandler<GetUserByUserIdQuery, Result>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByUserIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(GetUserByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByUserId(request.UserId);
                if (existingUser == null)
                {
                    return Result.Failure("Invalid user");
                }
                return Result.Success("User retrieval was successful", existingUser);
            }
            catch (Exception ex)
            {
                return Result.Failure(new string[] { "Retrieving user by Id was not successful", ex?.Message ?? ex?.InnerException.Message });
            }
        }
    }
}
