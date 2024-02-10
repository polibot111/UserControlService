using Application.Services.Infrastructure;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ILoginOperations, LoginOperations>();
            services.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}
