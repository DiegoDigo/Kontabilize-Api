using System;
using System.Threading.Tasks;
using Kontabilize.Domain.UserContext.Command.Input;
using Kontabilize.Domain.UserContext.Handlers;
using Kontabilize.Domain.UserContext.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kontabilize.Api.Controller
{
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Accountant,Customer")]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileHandler _handler;
        private readonly IProfileService _profileService;

        public ProfileController(ProfileHandler handler, IProfileService profileService)
        {
            _handler = handler;
            _profileService = profileService;
        }

        [HttpPost]
        [ApiVersion("1.0")]
        public async Task<IActionResult> CreateProfile([FromForm] CreateProfileCommand command)
        {
            var response = await _handler.Handler(command);
            return response.Success ? StatusCode(201, response) : StatusCode(400, response);
        }

        [HttpPut]
        [ApiVersion("1.0")]
        public async Task<IActionResult> EditProfile([FromForm] EditProfileCommand command)
        {
            var response = await _handler.Handler(command);
            return response.Success ? Ok(response) : StatusCode(400, response);
        }

        [HttpGet("{userId:Guid}")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var response = await _profileService.GetProfileByIdUser(userId);
            return response.Success ? Ok(response) : StatusCode(400, response);
        }
    }
}