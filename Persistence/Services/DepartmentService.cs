﻿using Application.CQRS.Department;
using Application.DTO.Department;
using Application.Repositories.Department;
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
    public class DepartmentService : IDepartmentService
    {
        public DepartmentService(IDepartmentReadRepo readDepartment, IDepartmentWriteRepo writeDepartment, IMapper mapper)
        {
            _readDepartment = readDepartment;
            _writeDepartment = writeDepartment;
            _mapper = mapper;
        }
        readonly private IDepartmentReadRepo _readDepartment;
        readonly private IDepartmentWriteRepo _writeDepartment;
        readonly private IMapper _mapper;


        public async Task<IQueryable<DepartmentDTO>> GetAllAsync()
        {
            var Departments = await _readDepartment.GetAll();
            IQueryable<DepartmentDTO> departmentDTOs = Departments.ProjectTo<DepartmentDTO>(_mapper.ConfigurationProvider); 
            return departmentDTOs;
        }

        public async Task<DepartmentGetByIdDTO> GetByIdAsync(DepartmentDetailQuery request)
        {
            var Department = await _readDepartment.GetByIdAsync(request.Id);
            DepartmentGetByIdDTO DepartmentDTO = _mapper.Map<DepartmentGetByIdDTO>(Department);
            return DepartmentDTO;
        }

        public async Task<bool> AddAsync(DepartmentInsertCommand request)
        {
            var result = await _writeDepartment.AddAsync(new()
            {
                DepartmentName = request.DepartmentName

            });
            await _writeDepartment.SaveAsync();
            return result;
        }

        public async Task<bool> UpdateAsync(DepartmentUpdateCommand request)
        {
            var result = await _writeDepartment.UpdateAsync(new()
            {
                Id = request.Id,
                DepartmentName = request.DepartmentName
            }) ;
            await _writeDepartment.SaveAsync();
            return result;
                 
        }

        public async Task<bool> UpdateStatusAsync(DepartmentUpdateStatusCommand request)
        {
            var result = await _writeDepartment.UpdateStatusAsync(request.Id);
            await _writeDepartment.SaveAsync();
            return result;

        }
    }
}
