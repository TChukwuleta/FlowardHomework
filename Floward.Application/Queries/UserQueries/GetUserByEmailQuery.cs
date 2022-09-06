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
    public class GetUserByEmailQuery : IRequest<Result>
    {
        public string Email { get; set; }
    }

    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, Result>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByEmail(request.Email);
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
