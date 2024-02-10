using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Persistence.UserDetail
{
    public class UserDetailDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string DepartmentName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
