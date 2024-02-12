using Application.DTO.Infrastructure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Infrastructure
{
    public interface ITokenHandler
    {
        UserToken CreateAccessToken(User user);
        string CreateRefreshToken();
    }
}
