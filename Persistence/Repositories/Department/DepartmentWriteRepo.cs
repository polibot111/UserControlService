using Application.Repositories.Department;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Department
{
    public class DepartmentWriteRepo : WriteRepository<Domain.Entities.Department>,IDepartmentWriteRepo
    {
        public DepartmentWriteRepo(ProjectDbContext context): base(context)
        {
                
        }
    }
}
