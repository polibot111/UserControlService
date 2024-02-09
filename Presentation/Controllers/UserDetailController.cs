using Application.CQRS.UserDetail;
using Application.DTO.UserDetail;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {
        public UserDetailController(IUserDetailService service)
        {
            _service = service;
        }
        readonly private IUserDetailService _service;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] UserDetailQuery request)
        {
            return Ok(await _service.GetById(request));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserDetailInsertCommand request)
        {
            return Ok(await _service.AddAsync(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDetailUpdateCommand request)
        {
            return Ok(await _service.UpdateAsync(request));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] UserDetailUpdateStatusCommand request)
        {
            return Ok(await _service.UpdateStatusAsync(request));
        }
    }
}
