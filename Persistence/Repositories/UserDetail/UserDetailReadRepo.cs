
using Application.Repositories.UserDetail;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.UserDetail
{
    public class UserDetailReadRepo : ReadRepository<Domain.Entities.UserDetail>, IUserDetailReadRepo
    {
        public UserDetailReadRepo(ProjectDbContext context) : base(context)
        {

        }
    }
}
