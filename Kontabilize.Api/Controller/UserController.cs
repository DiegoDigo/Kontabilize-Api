using System.Threading.Tasks;
using Kontabilize.Domain.UserContext.Command.Input;
using Kontabilize.Domain.UserContext.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kontabilize.Api.Controller
{
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserHandler _userHandler;

        public UserController(UserHandler userHandler)
        {
            _userHandler = userHandler;
        }

        [HttpPost]
        [ApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
        {
            var response = await _userHandler.Handler(command);
            return response.Success ? Ok(response) : StatusCode(406, response);
        }

        [HttpPost("signUp")]
        [ApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
        {
            var response = await _userHandler.Handler(command);
            return response.Success ? StatusCode(201, response) : StatusCode(406, response);
        }
        
        [HttpPost("reset")]
        [ApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<IActionResult> Reset([FromBody] ResetPasswordCommand command)
        {
            var response = await _userHandler.Handler(command);
            return response.Success ? StatusCode(200, response) : StatusCode(406, response);
        }
    }
}