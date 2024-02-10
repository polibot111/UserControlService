using Application.CQRS.Infrastructure.Login;
using Application.DTO.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Infrastructure
{
    public interface ILoginOperations
    {
        Task<UserToken> Login(LoginRequest request);
        Task<UserToken> RefreshTokenLogin(LoginWithRefreshTokenRequest refreshToken);
    }
}
