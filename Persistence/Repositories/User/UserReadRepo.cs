
using Application.Repositories.User;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.User
{
    public class UserReadRepo : ReadRepository<Domain.Entities.User>, IUserReadRepo
    {
        public UserReadRepo(ProjectDbContext context) : base(context)
        {

        }
    }
}
