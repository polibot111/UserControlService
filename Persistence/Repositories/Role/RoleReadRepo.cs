
using Application.Repositories.Role;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Role
{
    public class RoleReadRepo : ReadRepository<Domain.Entities.Role>, IRoleReadRepo
    {
        public RoleReadRepo(ProjectDbContext context) : base(context)
        {

        }
    }
}
