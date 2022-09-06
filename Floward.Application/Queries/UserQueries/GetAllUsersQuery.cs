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
    public class GetAllUsersQuery : IRequest<Result>
    {
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result>
    {
        private readonly IUserRepository _userRepository;
        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var allUsers = await _userRepository.GetAllAsync();
                return Result.Success("Users retrieval was successful", allUsers);
            }
            catch (Exception ex)
            {
                return Result.Failure(new string[] { "Getting all users was not successful", ex?.Message ?? ex?.InnerException.Message });
            }
        }
    }
}
