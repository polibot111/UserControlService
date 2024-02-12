using Application.Consts.Exceptions;
using Application.CQRS.Persistence.Role;
using Application.DTO.Persistence.Role;
using Application.Services.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class RoleService : IRoleService
    {

        public RoleService(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        readonly RoleManager<Role> _roleManager;
        readonly private IMapper _mapper;
        public async Task<bool> CreateRoleAsync(RoleInsertCommand request)
        {
            #region RoleNameControl
            var existingRole = await _roleManager.FindByNameAsync(request.RoleName);
            if (existingRole != null && existingRole.Status == true)
            {
                throw new Exception(ExceptionMessages.RoleNameControl);
            }
            #endregion

            IdentityResult result = await _roleManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.RoleName
            });

            if (result.Succeeded)
            {
                return true;
            }
            StringBuilder sb = new StringBuilder();
            foreach (var role in result.Errors)
            {
                sb.AppendLine(role.Description + "\n");
            }

            throw new Exception(sb.ToString());
        



    }

    public async Task<IQueryable<RoleDTO>> GetAllAsync()
    {
        List<Role> roles = await _roleManager.Roles.ToListAsync();
        IQueryable queryableRoles = roles.AsQueryable();
        IQueryable<RoleDTO> result = queryableRoles.ProjectTo<RoleDTO>(_mapper.ConfigurationProvider);
        return result;
    }

    public async Task<RoleGetByIDDTO> GetById(RoleDetailQuery request)
    {
        var rol = _roleManager.Roles;
        Role? role = await _roleManager.FindByIdAsync(request.Id);
        RoleGetByIDDTO result = _mapper.Map<RoleGetByIDDTO>(role);
        return result;
    }


    public async Task<bool> UpdateStatusAsync(RoleUpdateStatusCommand request)
    {
        Role? role = await _roleManager.FindByIdAsync(request.Id);
        if (role != null)
        {
            role.Status = false;
            await _roleManager.UpdateAsync(role);
            return true;
        }
        return false;
    }
}
}
