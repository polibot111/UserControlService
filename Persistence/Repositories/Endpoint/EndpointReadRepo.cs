using Application.Repositories.Endpoint;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Endpoint
{
    public class EndpointReadRepo: ReadRepository<Domain.Entities.Endpoint>, IEndpointReadRepo
    {
        public EndpointReadRepo(ProjectDbContext context) : base(context)
        {

        }
    }
}
