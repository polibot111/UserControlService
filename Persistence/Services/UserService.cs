using Application.CQRS.Infrastructure.Login;
using Application.CQRS.Persistence.User;
using Application.CQRS.Persistence.Role;
using Application.DTO.Persistence.User;
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
using Application.Consts.Exceptions;
using Application.RequestParameters;
using Microsoft.EntityFrameworkCore;
using Application.PaginationParameters;
using Application.Repositories.Endpoint;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        public UserService(UserManager<User> userManager, IConfiguration configuration, RoleManager<Role> roleManager, IEndpointReadRepo endpointReadRepo)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _endpointReadRepo = endpointReadRepo;
        }

        readonly UserManager<User> _userManager;
        readonly RoleManager<Role> _roleManager;

        readonly IConfiguration _configuration;
        readonly IEndpointReadRepo _endpointReadRepo;

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

        public async Task<PaginationResult<UserDTO>> GetAllUsersWithPagination(Pagination pagination)
        {
            var query = await _userManager.Users.Include(x => x.Role).Include(x => x.Detail).ThenInclude(ud => ud.Department).ToListAsync();


            int totalCount = query.Count;
            int totalPages = (int)Math.Ceiling(totalCount / (double)pagination.Size);
            pagination.Page = Math.Min(Math.Max((int)pagination.Page, 1), totalPages);
            int startIndex = ((int)pagination.Page - 1) * (int)pagination.Size;

            var users = query
                        .Skip(startIndex)
                        .Take((int)pagination.Size).ToList();
            ;

            IQueryable<UserDTO> result = users.Select(user => new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                DepartmentName = user.Detail != null && user.Detail.Department != null ? user.Detail.Department.DepartmentName : null,
                RoleId = user.Role != null ? user.Role.Id : null,
                RoleName = user.Role != null ? user.Role.Name : null

            }).AsQueryable();

            return new()
            {
                Items = result,
                PageNumber = (int)pagination.Page,
                TotalCount = totalCount,
                PageSize = (int)pagination.Size
            };
        }

        public async Task<bool> UpdateUserRole(UserCommandForUserRole request)
        {
            User user = await _userManager.FindByIdAsync(request.UserId);
            if (user != null)
            {
                user.Role = await _roleManager.FindByIdAsync(request.RoleId);
                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return true;
                }

            }
            throw new Exception(ExceptionMessages.NotFoundUser);

        }

        public async Task<bool> UpdatePassword(UserCommandForPassword request)
        {
            User user = await _userManager.FindByIdAsync(request.Id);
            if (user != null)
            {

                IdentityResult result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

                if (result.Succeeded)
                {
                    return true;
                }
            }

            throw new Exception(ExceptionMessages.WrongPassword);
        }

        public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
        {
            User? userWithRole = await _userManager.Users.Include(x => x.Role).FirstOrDefaultAsync(u => u.UserName == name);

            if (userWithRole.Role == null) return false;

            Endpoint? endpoint = await _endpointReadRepo.Table
                .Include(e => e.Roles)
                .FirstOrDefaultAsync(e => e.Code == code);

            if (endpoint == null) return false;

            return true;
                
        }
    }
}
