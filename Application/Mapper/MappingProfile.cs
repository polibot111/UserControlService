using Application.DTO.Department;
using Application.DTO.Role;
using Application.DTO.User;
using Application.DTO.UserDetail;
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
            CreateMap<User, UserGetByIdDTO>()
                .ForMember(x => x.RoleId, opt => opt.MapFrom(src => src.Role.Id))
                .ForMember(x => x.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));

            CreateMap<UserDetail,UserDetailDTO>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(x => x.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName));

            CreateMap<UserDetail, UserDetailGetByIdDTO>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(x => x.Mail, opt => opt.MapFrom(src => src.User.Mail))
                .ForMember(x => x.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName));
                
          
        }
    }
}
