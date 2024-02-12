using Application.Consts.Authorize;
using Application.CQRS.Persistence.UserDetail;
using Application.CustomAttributes;
using Application.DTO.Persistence.UserDetail;
using Application.Enums;
using Application.Services.Persistence;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefination(Menu = AuthorizeDefinitionConstants.UserDetail, ActionType = ActionType.ReadingAll,
            Definition = "Get All Users Detail")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{Id}")]
        [AuthorizeDefination(Menu = AuthorizeDefinitionConstants.UserDetail, ActionType = ActionType.ReadingById,
            Definition = "Get Users Detail")]
        public async Task<IActionResult> GetById([FromRoute] UserDetailQuery request)
        {
            return Ok(await _service.GetById(request));
        }

        [HttpPost]
        [AuthorizeDefination(Menu = AuthorizeDefinitionConstants.UserDetail, ActionType = ActionType.Writing,
            Definition = "Create User Detail")]
        public async Task<IActionResult> Add([FromBody] UserDetailInsertCommand request)
        {
            return Ok(await _service.AddAsync(request));
        }

        [HttpPut]
        [AuthorizeDefination(Menu = AuthorizeDefinitionConstants.UserDetail, ActionType = ActionType.Updating,
            Definition = "Update User Detail")]
        public async Task<IActionResult> Update([FromBody] UserDetailUpdateCommand request)
        {
            return Ok(await _service.UpdateAsync(request));
        }

        [HttpDelete]
        [AuthorizeDefination(Menu = AuthorizeDefinitionConstants.UserDetail, ActionType = ActionType.Deleting,
            Definition = "Delete User Detail")]
        public async Task<IActionResult> Delete([FromBody] UserDetailUpdateStatusCommand request)
        {
            return Ok(await _service.UpdateStatusAsync(request));
        }
    }
}
