﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Persistence.Role
{
    public class RoleUpdateStatusCommand
    {
        public Guid Id { get; set; }
    }
}