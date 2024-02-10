using Application.CQRS.Persistence.Role;
using Application.DTO.Persistence.Role;
using Application.Services.Persistence;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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
        public Task<bool> AddAsync(RoleInsertCommand request)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<RoleDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<RoleGetByIDDTO> GetById(RoleDetailQuery request)
        {
            var rol = _roleManager.Roles;
            Role role = (Role)_roleManager.Roles.Where(x => x.Id == request.Id);
            RoleGetByIDDTO result =_mapper.Map<RoleGetByIDDTO>(role);
            return result;
        }

        public Task<bool> UpdateAsync(RoleUpdateCommand request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStatusAsync(RoleUpdateStatusCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
