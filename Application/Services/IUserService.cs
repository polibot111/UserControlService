
using Application.CQRS.User;
using Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        Task<IQueryable<UserDTO>> GetAllAsync();
        Task<UserGetByIdDTO> GetById(UserQuery request);
        Task<UserGetByIdDTO> GetByIdWithRoleId(UserQuery request);
        Task<bool> AddAsync(UserInsertCommand request);
        Task<bool> UpdateUserPasswordAsync(UserUpdateCommand request);
        Task<bool> UpdateStatusAsync(UserUpdateStatusCommand request);
    }
}
