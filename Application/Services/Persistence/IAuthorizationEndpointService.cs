using Application.CQRS.Persistence.AuthorizationEndpoint;
using Application.DTO.Persistence.Endpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Persistence
{
    public interface IAuthorizationEndpointService
    {
        public Task<bool> AssignEndpointRoleAsync(AssignRoleEndpointInsertCommand request, Type type);

        public Task<List<EndpointsToRoleDTO>> GetEndpointAsync(AssignedEndpointToRoleQuery request);
    }
}
