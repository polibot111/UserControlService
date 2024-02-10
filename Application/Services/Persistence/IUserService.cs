
using Application.CQRS.Infrastructure.Login;
using Application.CQRS.Persistence.User;
using Application.DTO.Persistence.User;
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
        Task<bool> CreateUser(UserInsertCommand request);
        Task UpdateRefreshToken(string refreshToken, User user, DateTime accessTokenDate);
    }
}
