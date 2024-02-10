using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Persistence.Role
{
    public class RoleUpdateCommand
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
    }
}
