using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Infrastructure.Login
{
    public class LoginWithRefreshTokenRequest
    {
        public string RefreshToken { get; set; }
    }
}
