﻿using Application.CQRS.Department;
using Application.DTO.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IDepartmentService
    {
        Task<IQueryable<DepartmentDTO>> GetAllAsync();
        Task<DepartmentGetByIdDTO> GetByIdAsync(DepartmentDetailQuery request);
        Task<bool> AddAsync(DepartmentInsertCommand request);
        Task<bool> UpdateAsync(DepartmentUpdateCommand request);
        Task<bool> UpdateStatusAsync(DepartmentUpdateStatusCommand request);
    }
}