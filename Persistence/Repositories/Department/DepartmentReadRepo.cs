using Application.Repositories.Department;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Department
{
    public class DepartmentReadRepo : ReadRepository<Domain.Entities.Department>, IDepartmentReadRepo
    {
        public DepartmentReadRepo(ProjectDbContext context) : base(context)
        {

        }
    }
}
