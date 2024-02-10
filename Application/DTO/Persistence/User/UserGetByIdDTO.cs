using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Persistence.User
{
    public class UserGetByIdDTO
    {
        public Guid Id { get; set; }
        public string Mail { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
