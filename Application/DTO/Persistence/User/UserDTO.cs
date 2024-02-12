using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Persistence.User
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentId { get; set; }
    }
}
