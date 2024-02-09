using Application.Repositories.Department;
using Application.Repositories.Role;
using Application.Repositories.User;
using Application.Repositories.UserDetail;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories.Department;
using Persistence.Repositories.Role;
using Persistence.Repositories.User;
using Persistence.Repositories.UserDetail;
using Persistence.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ProjectDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));

            #region Repostories

            services.AddScoped<IDepartmentReadRepo, DepartmentReadRepo>();
            services.AddScoped<IDepartmentWriteRepo, DepartmentWriteRepo>();
            services.AddScoped<IRoleReadRepo, RoleReadRepo>();
            services.AddScoped<IRoleWriteRepo, RoleWriteRepo>();
            services.AddScoped<IUserReadRepo, UserReadRepo>();
            services.AddScoped<IUserWriteRepo, UserWriteRepo>();
            services.AddScoped<IUserDetailReadRepo, UserDetailReadRepo>();
            services.AddScoped<IUserDetailWriteRepo, UserDetailWriteRepo>();

            #endregion

            #region Services

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserDetailService, UserDetailService>();

            #endregion


        }
    }
}
