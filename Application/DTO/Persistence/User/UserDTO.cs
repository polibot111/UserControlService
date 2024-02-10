using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Persistence.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Mail { get; set; }
        public Guid RoleId { get; set; }
    }
}
