using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Persistence.User
{
    public class UserUpdateStatusCommand
    {
        public Guid Id { get; set; }
    }
}
