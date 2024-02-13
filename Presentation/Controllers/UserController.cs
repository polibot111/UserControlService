using Application.Consts.Authorize;
using Application.CQRS.Infrastructure.Login;
using Application.CQRS.Persistence.User;
using Application.CustomAttributes;
using Application.Enums;
using Application.RequestParameters;
using Application.Services.Infrastructure;
using Application.Services.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(IUserService userService, ILoginOperations loginOperations)
        {
            _userService = userService;
            _loginOperations = loginOperations;
        }
        readonly private IUserService _userService;
        readonly private ILoginOperations _loginOperations;

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefination(Menu = AuthorizeDefinitionConstants.User, ActionType = ActionType.ReadingAll,
            Definition = "Read All User With Pagination")]
        public async Task<IActionResult> GetAllUsersWithPagination([FromQuery] Pagination pagination)
        {
            return Ok(await _userService.GetAllUsersWithPagination(pagination));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUser([FromBody] UserInsertCommand request)
        {
            return Ok(await _userService.CreateUser(request));
        }

        [HttpPost("[action]")]
        [AuthorizeDefination(Menu = AuthorizeDefinitionConstants.User, ActionType = ActionType.Updating,
            Definition = "Update User Role")]
        public async Task<IActionResult> UserRoleUpdate([FromBody] UserCommandForUserRole request)
        {
            return Ok(await _userService.UpdateUserRole(request));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdatePassword([FromBody] UserCommandForPassword request)
        {
            return Ok(await _userService.UpdatePassword(request));
        }
    }
}
