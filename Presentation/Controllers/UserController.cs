using Application.CQRS.Role;
using Application.CQRS.User;
using Application.DTO.User;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(IUserService service)
        {
            _service = service;
        }
        readonly private IUserService _service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] UserQuery request)
        {
            return Ok(await _service.GetByIdWithRoleId(request));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserInsertCommand request)
        {
            return Ok(await _service.AddAsync(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserUpdateCommand request)
        {
            return Ok(await _service.UpdateUserPasswordAsync(request));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] UserUpdateStatusCommand request)
        {
            return Ok(await _service.UpdateStatusAsync(request));
        }
    }
}
