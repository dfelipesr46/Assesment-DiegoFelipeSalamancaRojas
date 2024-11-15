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

        [HttpPost("register/doctor")]
        public async Task<IActionResult> RegisterDoctor([FromBody] DoctorRegistrationDto doctorRegistrationDto)
        {
            var result = await _authService.RegisterDoctorAsync(doctorRegistrationDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPost("register/patient")]
        public async Task<IActionResult> RegisterPatient([FromBody] PatientRegistrationDto patientRegistrationDto)
        {
            var result = await _authService.RegisterPatientAsync(patientRegistrationDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
