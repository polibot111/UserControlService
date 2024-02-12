﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Persistence.AuthorizationEndpoint
{
    public class AssignRoleEndpointInsertCommand
    {
        public List<string> EndpointCodes { get; set; } = new();
        public string RoleId { get; set; }
    }
}
