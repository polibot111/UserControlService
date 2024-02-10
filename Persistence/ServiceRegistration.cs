using Application.Repositories.Department;
using Application.Repositories.UserDetail;
using Application.Services.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories.Department;
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

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<ProjectDbContext>();

            #region Repostories

            services.AddScoped<IDepartmentReadRepo, DepartmentReadRepo>();
            services.AddScoped<IDepartmentWriteRepo, DepartmentWriteRepo>();
            services.AddScoped<IUserDetailReadRepo, UserDetailReadRepo>();
            services.AddScoped<IUserDetailWriteRepo, UserDetailWriteRepo>();

            #endregion

            #region Services

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IUserDetailService, UserDetailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            #endregion


        }
    }
}
