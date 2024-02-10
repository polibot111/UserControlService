using Application.CQRS.Infrastructure.Login;
using Application.CQRS.Persistence.User;
using Application.Services.Infrastructure;
using Application.Services.Persistence;
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


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserInsertCommand request)
        {
            return Ok(await _userService.CreateUser(request));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            return Ok(await _loginOperations.Login(request));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] LoginWithRefreshTokenRequest request)
        {
            return Ok(await _loginOperations.RefreshTokenLogin(request));
        }
    }
}
