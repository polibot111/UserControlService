using Application.DTO.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Infrastructure.Configuration
{
    public interface IApplicationService
    {
        List<MenuDTO> GetAuthorizeDefinitionEndPoints(Type type);
    }
}
