using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Role
{
    public class RoleUpdateCommand
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
    }
}
