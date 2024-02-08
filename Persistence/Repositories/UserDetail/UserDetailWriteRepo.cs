
using Application.Repositories.UserDetail;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.UserDetail
{
    public class UserDetailWriteRepo : WriteRepository<Domain.Entities.UserDetail>, IUserDetailWriteRepo
    {
        public UserDetailWriteRepo(ProjectDbContext context) : base(context)
        {

        }
    }
}
