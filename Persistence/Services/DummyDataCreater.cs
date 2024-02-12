using Application.Services.Persistence;
using AutoMapper.Execution;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class DummyDataCreater
    {

        readonly IRoleService _roleService;
        readonly IUserService _userService;
        readonly IUserDetailService _userDetailService;
        readonly IDepartmentService _departmentService;
        readonly IServiceProvider _serviceProvider;
        private readonly UserManager<User> _userManager;
        public DummyDataCreater(IRoleService roleService, IUserService userService, IUserDetailService userDetailService, IDepartmentService departmentService, IServiceProvider serviceProvider, UserManager<User> userManager)
        {
            _roleService = roleService;
            _userService = userService;
            _userDetailService = userDetailService;
            _departmentService = departmentService;
            _serviceProvider = serviceProvider;
            _userManager = userManager;
        }

        public async Task Create()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();

                if (!dbContext.Roles.Any())
                {
                    await _roleService.CreateRoleAsync(new()
                    {
                        RoleName = "SuperAdmin"
                    });

                    await _roleService.CreateRoleAsync(new()
                    {
                        RoleName = "Admin"
                    });

                    await _roleService.CreateRoleAsync(new()
                    {
                        RoleName = "Member"
                    });

                    await _userService.CreateUser(new()
                    {
                       UserName = "superadmin",
                       Email ="super@admin.com",
                       Password = "123456"
                    });

                    await _userService.CreateUser(new()
                    {
                        UserName = "admin",
                        Email = "admin@admin.com",
                        Password = "123456"
                    });

                    for (int i = 0;  i < 30;  i++)
                    {
                        await _userService.CreateUser(new()
                        {
                            UserName = $"member{i}",
                            Email = $"member{i}@member.com",
                            Password = "123456"
                        });
                    }

                    await _userService.CreateUser(new()
                    {
                        UserName = "member",
                        Email = "member@member.com",
                        Password = "123456"
                    });

                    for (int i = 0; i < 15; i++)
                    {
                        await _departmentService.AddAsync(new()
                        {
                            DepartmentName = $"Department.{i}"
                        });
                    }

                    var department = await _departmentService.GetAllAsync();
                    var departmentList = department.ToList();
                    Random random = new Random();

                    for (int i = 0; i < 30; i++)
                    {
                        int randomNumber = random.Next(0, 14 + 1);
                        var user = await _userManager.FindByNameAsync($"member{i}");
                        
                        await _userDetailService.AddAsync(new()
                        {
                            Name = $"NameMember{i}",
                            Surname = $"SurnameMember{i}",
                            PhoneNumber = $"{i}{i}{i}{i}{i}{i}{i}{i}{i}{i}{i}{i}",
                            UserId = Guid.Parse(user.Id),
                            DepartmentId = departmentList[randomNumber].Id,

                        });
                      
                    }


                }
            }
        }
    }
}
