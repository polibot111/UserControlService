using Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : IdentityUser<string>
    {
        public bool Status { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDay { get; set; }

    }
}
