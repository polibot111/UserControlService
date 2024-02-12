using Application.Repositories.Endpoint;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Endpoint
{
    public class EndpointWriteRepo: WriteRepository<Domain.Entities.Endpoint>, IEndpointWriteRepo
    {
        public EndpointWriteRepo(ProjectDbContext context) : base(context)
        {

        }
    }
}
