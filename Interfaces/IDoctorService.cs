using System.Security.Claims;
using Assesment_DiegoFelipeSalamancaRojas.DTOs;

namespace Assesment_DiegoFelipeSalamancaRojas.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<AppointmentDto>> GetAppointmentsAsync(ClaimsPrincipal user, string period);
        Task<OperationResultDto> UpdateAvailabilityAsync(ClaimsPrincipal user, AvailabilityDto availabilityDto);
        Task<IEnumerable<PatientDto>> SearchPatientsAsync(string searchTerm);
        Task<PatientProfileDto?> GetPatientProfileAsync(int patientId);
    }
}
