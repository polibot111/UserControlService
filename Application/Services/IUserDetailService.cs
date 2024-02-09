
using Application.CQRS.UserDetail;
using Application.DTO.UserDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserDetailService
    {
        Task<IQueryable<UserDetailDTO>> GetAllAsync();
        Task<UserDetailGetByIdDTO> GetById(UserDetailQuery request);
        Task<bool> AddAsync(UserDetailInsertCommand request);
        Task<bool> UpdateAsync(UserDetailUpdateCommand request);
        Task<bool> UpdateStatusAsync(UserDetailUpdateStatusCommand request);
    }
}
