using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Department
{
    public class DepartmentUpdateStatusCommand
    {
        public string Id { get; set; }
    }
}
