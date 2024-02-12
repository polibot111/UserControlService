
using Application.CQRS.Infrastructure.Login;
using Application.CQRS.Persistence.User;
using Application.DTO.Persistence.User;
using Application.PaginationParameters;
using Application.RequestParameters;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Persistence
{
    public interface IUserService
    {
        Task<PaginationResult<UserDTO>> GetAllUsersWithPagination(Pagination pagination);
        Task<bool> UpdateUserRole(UserCommandForUserRole user);
        Task<bool> UpdatePassword(UserCommandForPassword user);
        Task<bool> CreateUser(UserInsertCommand request);
        Task UpdateRefreshToken(string refreshToken, User user, DateTime accessTokenDate);
        Task<bool> HasRolePermissionToEndpointAsync(string name, string code);
    }
}
