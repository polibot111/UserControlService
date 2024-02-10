using Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role: IdentityRole<string>
    {
        public string RoleName { get; set; }
        public bool Status { get; set; }
    }
}
