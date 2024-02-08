
using Application.Repositories.User;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.User
{
    public class UserWriteRepo : WriteRepository<Domain.Entities.User>, IUserWriteRepo
    {
        public UserWriteRepo(ProjectDbContext context) : base(context)
        {

        }
    }
}
