using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Assesment_DiegoFelipeSalamancaRojas.DTOs;
using Assesment_DiegoFelipeSalamancaRojas.Interfaces;

namespace Assesment_DiegoFelipeSalamancaRojas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Patient")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost("schedule-appointment")]
        public async Task<IActionResult> ScheduleAppointment([FromBody] AppointmentCreationDto appointmentDto)
        {
            var result = await _patientService.ScheduleAppointmentAsync(appointmentDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpGet("appointments")]
        public async Task<IActionResult> GetAppointments()
        {
            var result = await _patientService.GetAppointmentsAsync();
            return Ok(result);
        }

        [HttpPut("reschedule-appointment/{id}")]
        public async Task<IActionResult> RescheduleAppointment(int id, [FromBody] AppointmentRescheduleDto rescheduleDto)
        {
            var result = await _patientService.RescheduleAppointmentAsync(id, rescheduleDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpDelete("cancel-appointment/{id}")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var result = await _patientService.CancelAppointmentAsync(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpGet("appointment-history")]
        public async Task<IActionResult> GetAppointmentHistory()
        {
            var result = await _patientService.GetAppointmentHistoryAsync();
            return Ok(result);
        }
    }
}
