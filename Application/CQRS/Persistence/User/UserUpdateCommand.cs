using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Persistence.User
{
    public class UserUpdateCommand
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
    }
}
