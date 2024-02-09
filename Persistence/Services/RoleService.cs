using Application.CQRS.Role;
using Application.DTO.Department;
using Application.DTO.Role;
using Application.Repositories.Role;
using Application.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class RoleService : IRoleService
    {
        public RoleService(IRoleReadRepo readRepo, IMapper mapper, IRoleWriteRepo writeRepo)
        {
            _readRepo = readRepo;
            _mapper = mapper;
            _writeRepo = writeRepo;
        }

        readonly private IRoleReadRepo _readRepo;
        readonly private IRoleWriteRepo _writeRepo;
        readonly private IMapper _mapper;

        public async Task<IQueryable<RoleDTO>> GetAllAsync()
        {
            var roles = await _readRepo.GetAll();
            IQueryable<RoleDTO> result = roles.ProjectTo<RoleDTO>(_mapper.ConfigurationProvider);
            return result;
        }

        public async Task<RoleGetByIDDTO> GetById(RoleDetailQuery request)
        {
            var roles = await _readRepo.GetByIdAsync(request.Id.ToString());
            RoleGetByIDDTO result = _mapper.Map<RoleGetByIDDTO>(roles);
            return result;
        }

        public async Task<bool> AddAsync(RoleInsertCommand request)
        {
            var result = await _writeRepo.AddAsync(new() { RoleName = request.RoleName });
            await _writeRepo.SaveAsync();
            return result;
        }

        public async Task<bool> UpdateAsync(RoleUpdateCommand request)
        {
            var result = await _writeRepo.UpdateAsync(new() { Id = request.Id, RoleName = request.RoleName });
            await _writeRepo.SaveAsync();
            return result;
        }

        public async Task<bool> UpdateStatusAsync(RoleUpdateStatusCommand request)
        {
            var result = await _writeRepo.UpdateStatusAsync(request.Id.ToString());
            await _writeRepo.SaveAsync();
            return result;
        }

    }
}
