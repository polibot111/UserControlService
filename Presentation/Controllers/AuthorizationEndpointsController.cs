using Application.CQRS.Persistence.AuthorizationEndpoint;
using Application.Services.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class AuthorizationEndpointsController : ControllerBase
    {
        readonly IAuthorizationEndpointService _service;

        public AuthorizationEndpointsController(IAuthorizationEndpointService service)
        {
            _service = service;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEndpointsToRole([FromRoute]AssignedEndpointToRoleQuery request)
        {
            return Ok(await _service.GetEndpointAsync(request));
        }

        [HttpPost]
        public async Task<IActionResult> AssignEntpointRole([FromBody]AssignRoleEndpointInsertCommand request)
        {
            return Ok(await _service.AssignEndpointRoleAsync(request, typeof(Program)));
        }
    }
}
