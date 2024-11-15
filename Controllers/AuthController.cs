using Microsoft.AspNetCore.Mvc;
using Assesment_DiegoFelipeSalamancaRojas.Interfaces;
using Assesment_DiegoFelipeSalamancaRojas.DTOs;

namespace Assesment_DiegoFelipeSalamancaRojas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto userRegistrationDto)
        {
            var result = await _authService.RegisterUserAsync(userRegistrationDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var result = await _authService.LoginUserAsync(userLoginDto);
            if (!result.Success)
            {
                return Unauthorized(result.Message);
            }
            return Ok(result);
        }
    }
}
