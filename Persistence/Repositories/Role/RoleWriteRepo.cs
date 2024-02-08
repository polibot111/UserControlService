
using Application.Repositories.Role;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Role
{
    public class RoleWriteRepo : WriteRepository<Domain.Entities.Role>, IRoleWriteRepo
    {
        public RoleWriteRepo(ProjectDbContext context) : base(context)
        {

        }
    }
}
