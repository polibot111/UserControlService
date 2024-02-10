using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Persistence.UserDetail
{
    public class UserDetailUpdateStatusCommand
    {
        public Guid Id { get; set; }
    }
}
