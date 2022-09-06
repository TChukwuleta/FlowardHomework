﻿using Floward.Domain.Entities;
using Floward.Domain.Enums;
using Floward.Domain.Interfaces.IRepositories;
using Floward.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floward.Application.Commands.UserCommands
{
    public class CreateUserCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string UserName { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByEmail(request.Email);
                if (existingUser != null)
                {
                    return Result.Failure("User already exist");
                }
                var newUser = new ApplicationUser
                {
                    Email = request.Email,
                    Status = Status.Active,
                    StatusDesc = Status.Active.ToString(),
                    CreatedDate = DateTime.Now,
                    UserId = new Guid().ToString()
                };
                var result = await _userRepository.AddAsync(newUser);
                return Result.Success("User creation was successful", result);
            }
            catch (Exception ex)
            {
                return Result.Failure(new string[] { "User creation was not successful", ex?.Message ?? ex?.InnerException.Message });
            }
        }
    }
}


