
using Application.CQRS.UserDetail;
using Application.DTO.UserDetail;
using Application.Repositories.Department;
using Application.Repositories.User;
using Application.Repositories.UserDetail;
using Application.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class UserDetailService : IUserDetailService
    {
        public UserDetailService(IUserDetailReadRepo userDetailReadRepo, IUserDetailWriteRepo userDetailWriteRepo, IDepartmentReadRepo departmentReadRepo
            , IUserReadRepo userReadRepo, IMapper mapper)
        {
            _userDetailReadRepo = userDetailReadRepo;
            _userDetailWriteRepo = userDetailWriteRepo;
            _departmentReadRepo = departmentReadRepo;
            _userReadRepo = userReadRepo;
            _mapper = mapper;
        }
        readonly private IUserDetailReadRepo _userDetailReadRepo;
        readonly private IUserDetailWriteRepo _userDetailWriteRepo;
        readonly private IUserReadRepo _userReadRepo;
        readonly private IDepartmentReadRepo _departmentReadRepo;
        readonly private IMapper _mapper;

        public async Task<IQueryable<UserDetailDTO>> GetAllAsync()
        {
            var userDetails = await _userDetailReadRepo.GetAllWithIncludeAsync(
           predicate: x => true,
           includes: new Expression<Func<UserDetail, object>>[]
           {
                    x => x.User,
                    x => x.Department
           });

            IQueryable<UserDetailDTO> result = userDetails.ProjectTo<UserDetailDTO>(_mapper.ConfigurationProvider);
            return result;

        }

        public async Task<UserDetailGetByIdDTO> GetById(UserDetailQuery request)
        {
            var userDetail = await _userDetailReadRepo.GetSingleWithIncludeAsync(
               predicate: userDetail => userDetail.Id == request.Id,
               includes: new Expression<Func<UserDetail, object>>[]
               {
                  x => x.User,
                   x => x.Department
               });

            UserDetailGetByIdDTO result = _mapper.Map<UserDetailGetByIdDTO>(userDetail);
            return result;

        }

        public async Task<bool> AddAsync(UserDetailInsertCommand request)
        {
            bool result = await _userDetailWriteRepo.AddAsync(new()
            {
                Name = request.Name,
                Surname = request.Surname,
                PhoneNumber = request.PhoneNumber,
                Department = await _departmentReadRepo.GetByIdAsync(request.DepartmentId.ToString()),
                User = await _userReadRepo.GetByIdAsync(request.UserId.ToString())
            });
            await _userDetailWriteRepo.SaveAsync();
            return result;

        }

        public async Task<bool> UpdateAsync(UserDetailUpdateCommand request)
        {
            var userDetail = await _userDetailReadRepo.GetByIdAsync(request.Id.ToString());

            if (request.Name != null)
            {
                userDetail.Name = request.Name;
            }
            if (request.Surname != null)
            {
                userDetail.Surname = request.Surname;
            }
            if (request.PhoneNumber != null)
            {
                userDetail.PhoneNumber = request.PhoneNumber;
            }
            await _userDetailWriteRepo.SaveAsync();

            return true;

        }

        public async Task<bool> UpdateStatusAsync(UserDetailUpdateStatusCommand request)
        {
            bool result = await _userDetailWriteRepo.UpdateStatusAsync(request.Id.ToString());
            await _userDetailWriteRepo.SaveAsync();
            return result;
        }


    }
}
