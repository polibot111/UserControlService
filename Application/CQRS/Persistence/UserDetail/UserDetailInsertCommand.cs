using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Persistence.UserDetail
{
    public class UserDetailInsertCommand
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid UserId { get; set; }
    }
}
