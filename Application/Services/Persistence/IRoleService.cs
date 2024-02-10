
using Application.CQRS.Persistence.Role;
using Application.DTO.Persistence.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Persistence
{
    public interface IRoleService
    {
        Task<IQueryable<RoleDTO>> GetAllAsync();
        Task<RoleGetByIDDTO> GetById(RoleDetailQuery request);
        Task<bool> AddAsync(RoleInsertCommand request);
        Task<bool> UpdateAsync(RoleUpdateCommand request);
        Task<bool> UpdateStatusAsync(RoleUpdateStatusCommand request);
    }
}
