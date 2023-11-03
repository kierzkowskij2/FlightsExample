using FlightsExample.Core.Dtos;
using FlightsExample.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightsExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PassengersController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly IUserService _userService;

        public PassengersController(IRegistrationService registrationService,
            IUserService userService)
        {
            _registrationService = registrationService;
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<RegisterResultDto> Register([FromBody]RegisterRequestDto registerRequestDto)
        {
            var result = await _registrationService.Register(registerRequestDto);
            return result;
        }

        [HttpPost]
        [Route("users")]
        public async Task<IEnumerable<UserDto>?> GetUsers([FromBody] string searchTerm)
        {
            var result = await _userService.GetUsers(searchTerm);
            return result;
        }
    }
}