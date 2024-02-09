using Application.CQRS.Role;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Services;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public RoleController(IRoleService roleService)
        {
            _service = roleService;
        }

        readonly private IRoleService _service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] RoleDetailQuery request)
        {
            return Ok(await _service.GetById(request));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RoleInsertCommand request)
        {
            return Ok(await _service.AddAsync(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RoleUpdateCommand request)
        {
            return Ok(await _service.UpdateAsync(request));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] RoleUpdateStatusCommand request)
        {
            return Ok(await _service.UpdateStatusAsync(request));
        }
    }
}
