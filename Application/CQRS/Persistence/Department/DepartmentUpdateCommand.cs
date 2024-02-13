﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Persistence.Department
{
    public class DepartmentUpdateCommand
    {
        public string Id { get; set; }
        public string DepartmentName { get; set; }
    }
}
