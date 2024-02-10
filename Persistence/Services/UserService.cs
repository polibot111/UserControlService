using Application.CQRS.Infrastructure.Login;
using Application.CQRS.Persistence.User;
using Application.CQRS.Persistence.Role;
using Application.DTO.Persistence.User;
using Application.Exceptions;
using Application.Services.Persistence;
using Application.Wrappers;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        public UserService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        readonly UserManager<User> _userManager;

        readonly IConfiguration _configuration;

        public async Task<bool> CreateUser(UserInsertCommand request)
        {
            var result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,

            }, request.Password);

            if (result.Succeeded)
            {
                return result.Succeeded;
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                foreach (var item in result.Errors)
                {
                    sb.AppendLine(item.Description);
                }

                string response = sb.ToString();
                throw new Exception(response);
            }
        }

        public async Task UpdateRefreshToken(string refreshToken, User user, DateTime accessTokenDate)
        {
            if (user != null)
            {
                int expireDaye = Convert.ToInt32(_configuration["RefreshTokenExpireDay"]);
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDay = DateTime.UtcNow.AddDays(expireDaye);

                await _userManager.UpdateAsync(user);
            }
            else
                throw new Exception(ExceptionMessages.NotFoundUser);
        }
    }
}
