using Application.CQRS.Persistence.Department;
using Application.DTO.Persistence.Department;
using Application.PaginationParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Persistence
{
    public interface IDepartmentService
    {
        Task<IQueryable<DepartmentDTO>> GetAllAsync();
        Task<PaginationResult<DepartmentDTO>> GetAllWithPaginationAsync(DepartmentDetailPaginationQuery query);
        Task<DepartmentGetByIdDTO> GetByIdAsync(DepartmentDetailQuery request);
        Task<bool> AddAsync(DepartmentInsertCommand request);
        Task<bool> UpdateAsync(DepartmentUpdateCommand request);
        Task<bool> UpdateStatusAsync(DepartmentUpdateStatusCommand request);
    }
}
