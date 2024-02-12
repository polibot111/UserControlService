using Application.Consts.Authorize;
using Application.CQRS.Persistence.Role;
using Application.CQRS.Persistence.UserDetail;
using Application.CustomAttributes;
using Application.Enums;
using Application.Services.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RoleController : ControllerBase
    {
        public RoleController(IRoleService roleService)
        {
            _service = roleService;
        }

        readonly private IRoleService _service;

        [HttpGet]
        [AuthorizeDefination(Menu = AuthorizeDefinitionConstants.Role, ActionType = ActionType.ReadingAll,
        Definition = "Get All Roles")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{Id}")]
        [AuthorizeDefination(Menu = AuthorizeDefinitionConstants.Role, ActionType = ActionType.ReadingById,
            Definition = "Get Role Detail")]
        public async Task<IActionResult> GetById([FromRoute] RoleDetailQuery request)
        {
            return Ok(await _service.GetById(request));
        }

        [HttpPost]
        [AuthorizeDefination(Menu = AuthorizeDefinitionConstants.Role, ActionType = ActionType.Writing,
            Definition = "Create Role")]
        public async Task<IActionResult> Add([FromBody] RoleInsertCommand request)
        {
            return Ok(await _service.CreateRoleAsync(request));
        }


        [HttpDelete]
        [AuthorizeDefination(Menu = AuthorizeDefinitionConstants.Role, ActionType = ActionType.Deleting,
            Definition = "Delete Role")]
        public async Task<IActionResult> Delete([FromBody] RoleUpdateStatusCommand request)
        {
            return Ok(await _service.UpdateStatusAsync(request));
        }
    }
}
