using Assesment_DiegoFelipeSalamancaRojas.DTOs;

namespace Assesment_DiegoFelipeSalamancaRojas.Interfaces
{
    public interface IPatientService
    {
        Task<OperationResultDto> ScheduleAppointmentAsync(AppointmentCreationDto dto);
        Task<IEnumerable<AppointmentDto>> GetAppointmentsAsync();
        Task<OperationResultDto> RescheduleAppointmentAsync(int appointmentId, AppointmentRescheduleDto dto);
        Task<OperationResultDto> CancelAppointmentAsync(int appointmentId);
        Task<IEnumerable<AppointmentHistoryDto>> GetAppointmentHistoryAsync();
    }
}
