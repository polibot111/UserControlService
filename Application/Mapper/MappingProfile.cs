using Application.DTO.Persistence.Department;
using Application.DTO.Persistence.Role;
using Application.DTO.Persistence.User;
using Application.DTO.Persistence.UserDetail;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDTO>();
            CreateMap<Department, DepartmentGetByIdDTO>();
            CreateMap<Role, RoleDTO>();
            CreateMap<Role, RoleGetByIDDTO>();
            CreateMap<User, UserDTO>();


            CreateMap<UserDetail,UserDetailDTO>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(x => x.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName));

            CreateMap<UserDetail, UserDetailGetByIdDTO>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(x => x.Mail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(x => x.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName));
                
          
        }
    }
}
