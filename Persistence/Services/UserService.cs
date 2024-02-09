using Application.CQRS.User;
using Application.DTO.User;
using Application.Repositories.Role;
using Application.Repositories.User;
using Application.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        public UserService(IUserReadRepo readRepo, IUserWriteRepo writeRepo, IMapper mapper, IRoleReadRepo roleReadRepo)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
            _mapper = mapper;
            _roleReadRepo = roleReadRepo;
        }

        readonly private IUserReadRepo _readRepo;
        readonly private IUserWriteRepo _writeRepo;
        readonly private IRoleReadRepo _roleReadRepo;
        readonly private IMapper _mapper;

        public async Task<IQueryable<UserDTO>> GetAllAsync()
        {
            try
            {
                var users = await _readRepo.GetAll();
                IQueryable<UserDTO> result = users.ProjectTo<UserDTO>(_mapper.ConfigurationProvider);
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<UserGetByIdDTO> GetById(UserQuery request)
        {

            var users = await _readRepo.GetWhere(x => x.Id == request.Id);
            IQueryable<UserGetByIdDTO> results = users.ProjectTo<UserGetByIdDTO>(_mapper.ConfigurationProvider);
            UserGetByIdDTO result = results.First();

            return result;


        }

        public async Task<UserGetByIdDTO> GetByIdWithRoleId(UserQuery request)
        {

            var users = await _readRepo.GetSingleWithIncludeAsync(
                predicate: user => user.Id == request.Id,
                includes: user => user.Role);
            UserGetByIdDTO result = _mapper.Map<UserGetByIdDTO>(users);


            return result;


        }

        public async Task<bool> AddAsync(UserInsertCommand request)
        {

            using MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            StringBuilder password = new StringBuilder();

            foreach (var item in data)
            {
                password.Append(item.ToString("x2").ToLower());
            }

            bool result = await _writeRepo.AddAsync(new()
            {
                Mail = request.Mail,
                Password = password.ToString(),
                Role = await _roleReadRepo.GetByIdAsync(request.RoleId.ToString())
            });
            await _writeRepo.SaveAsync();
            return result;
        }

        public async Task<bool> UpdateUserPasswordAsync(UserUpdateCommand request)
        {
            using MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            StringBuilder password = new StringBuilder();

            foreach (var item in data)
            {
                password.Append(item.ToString("x2").ToLower());
            }

            var user = await _readRepo.GetByIdAsync(request.Id.ToString());
            user.Password = password.ToString();

            await _writeRepo.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(UserUpdateStatusCommand request)
        {
            bool result = await _writeRepo.UpdateStatusAsync(request.Id.ToString());
            await _writeRepo.SaveAsync();
            return result;
        }
    }
}
