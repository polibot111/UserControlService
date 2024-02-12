using Application.CQRS.Persistence.AuthorizationEndpoint;
using Application.DTO.Infrastructure.Configuration;
using Application.DTO.Persistence.Department;
using Application.DTO.Persistence.Endpoint;
using Application.Repositories.Endpoint;
using Application.Services.Infrastructure.Configuration;
using Application.Services.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        readonly IApplicationService _applicationService;
        readonly IEndpointReadRepo _endpointReadRepo;
        readonly IEndpointWriteRepo _endpointWriteRepo;
        readonly IMapper _mapper;
        readonly RoleManager<Role> _roleManager;

        public AuthorizationEndpointService(IApplicationService applicationService, IEndpointReadRepo endpointReadRepo, IEndpointWriteRepo endpointWriteRepo, RoleManager<Role> roleManager, IMapper mapper)
        {
            _applicationService = applicationService;
            _endpointReadRepo = endpointReadRepo;
            _endpointWriteRepo = endpointWriteRepo;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<bool> AssignEndpointRoleAsync(AssignRoleEndpointInsertCommand request, Type type)
        {
            List<Endpoint> endpointList = (await _endpointReadRepo.GetAll()).ToList();

            List<string> uncommonCodes = new List<string>();

            if (endpointList.Count != 0)
            {
                //uncommonCodes = endpointList.Where(x => !request.EndpointCodes.Contains(x.Code)).Select(x => x.Code).ToList();
                uncommonCodes = request.EndpointCodes.Except(endpointList.Select(x => x.Code)).ToList();
            }
            else
            {
                uncommonCodes = request.EndpointCodes;
            }


            if (uncommonCodes.Count != 0)
            {
                var endpoints = _applicationService.GetAuthorizeDefinitionEndPoints(type);
                //var uncommonEndpoints = endpoints.Where(x => x.Actions.Any(z => uncommonCodes.Contains(z.Code))).ToList();

                List<MenuDTO> matchedMenus = endpoints
                                            .Select(menu => new MenuDTO
                                            {
                                                Name = menu.Name,
                                                Actions = menu.Actions.Where(action => uncommonCodes.Contains(action.Code)).ToList()

                                            }).Where(menu => menu.Actions.Any())
                                             .ToList();



                List<Domain.Entities.Endpoint> _endpoints = new List<Domain.Entities.Endpoint>();

                foreach (var menu in matchedMenus)
                {

                    foreach (var item in menu.Actions)
                    {
                        Endpoint endpoint1 = new Endpoint();
                        endpoint1.ActionType = item.ActionType;
                        endpoint1.HttpType = item.HttpType;
                        endpoint1.Definition = item.Definition;
                        endpoint1.Code = item.Code;
                        endpoint1.Id = Guid.NewGuid();

                        endpoint1.Menu = menu.Name;

                        _endpoints.Add(endpoint1);
                    }

                }

                await _endpointWriteRepo.AddRangeAsync(_endpoints);

                await _endpointWriteRepo.SaveAsync();

                endpointList = (await _endpointReadRepo.GetAll()).ToList();
            }

            endpointList = endpointList.Where(x => request.EndpointCodes.Contains(x.Code)).ToList();
            var role = await _roleManager.Roles.Include(e => e.Endpoint).FirstOrDefaultAsync(r => r.Id == request.RoleId);


            for (int i = role.Endpoint.Count - 1; i >= 0; i--)
            {
                var endpoint2 = role.Endpoint.ElementAt(i);
                role.Endpoint.Remove(endpoint2);
            }


            foreach (var endpoint in endpointList)
            {
                endpoint.Roles.Add(role);
                role.Endpoint.Add(endpoint);
            }


            await _endpointWriteRepo.SaveAsync();

            return true;
        }

        public async Task<List<EndpointsToRoleDTO>> GetEndpointAsync(AssignedEndpointToRoleQuery request)
        {

            Role role = await _roleManager.Roles.Include(e => e.Endpoint).FirstOrDefaultAsync(e => e.Id == request.Id);

            List<EndpointsToRoleDTO> result = new();
            EndpointsToRoleDTO resultMenu = new();

            if (role != null)
            {
                foreach (var item in role.Endpoint)
                {
                    if (result.Any(x => x.Menu == item.Menu))
                    {
                        resultMenu = result.Find(x => x.Menu == item.Menu);
                        resultMenu.EndpointCodeDesc.Add(item.Code, item.Definition);
                    }
                    else
                    {
                        resultMenu = new()
                        {
                            Menu = item.Menu,
                        };
                        resultMenu.EndpointCodeDesc.Add(item.Code, item.Definition);

                        result.Add(resultMenu);
                    }
                }
            }

            return result;

        }
    }
}
