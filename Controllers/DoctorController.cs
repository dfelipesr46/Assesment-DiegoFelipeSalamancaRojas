using Assesment_DiegoFelipeSalamancaRojas.DTOs;
using Assesment_DiegoFelipeSalamancaRojas.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assesment_DiegoFelipeSalamancaRojas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        // 1. Get Appointments
        [HttpGet("appointments")]
        public async Task<IActionResult> GetAppointments([FromQuery] string period = "daily")
        {
            var appointments = await _doctorService.GetAppointmentsAsync(User, period);
            return Ok(appointments);
        }

        // 2. Manage Availability
        [HttpPut("availability")]
        public async Task<IActionResult> UpdateAvailability([FromBody] AvailabilityDto availabilityDto)
        {
            var result = await _doctorService.UpdateAvailabilityAsync(User, availabilityDto);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        // 3. Search Patients
        [HttpGet("patients")]
        public async Task<IActionResult> SearchPatients([FromQuery] string searchTerm)
        {
            var patients = await _doctorService.SearchPatientsAsync(searchTerm);
            return Ok(patients);
        }

        // 4. Get Patient Profile
        [HttpGet("patients/{id}")]
        public async Task<IActionResult> GetPatientProfile(int id)
        {
            var profile = await _doctorService.GetPatientProfileAsync(id);
            if (profile == null)
                return NotFound("Patient not found");

            return Ok(profile);
        }
    }
}
