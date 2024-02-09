using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.User
{
    public class UserInsertCommand
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
    }
}
