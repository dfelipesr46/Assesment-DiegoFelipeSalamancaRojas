using Assesment_DiegoFelipeSalamancaRojas.Interfaces;
using Assesment_DiegoFelipeSalamancaRojas.DTOs;
using Assesment_DiegoFelipeSalamancaRojas.Models;
using Assesment_DiegoFelipeSalamancaRojas.Data;
using Microsoft.EntityFrameworkCore;

namespace Assesment_DiegoFelipeSalamancaRojas.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _dbContext;

        public PatientService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResultDto> ScheduleAppointmentAsync(AppointmentCreationDto dto)
        {
            var overlappingAppointment = await _dbContext.Appointments
                .FirstOrDefaultAsync(a => a.DoctorId == dto.DoctorId && 
                                          a.AppointmentDate == dto.AppointmentDate);
            if (overlappingAppointment != null)
            {
                return new OperationResultDto
                {
                    Success = false,
                    Message = "The selected time slot is not available."
                };
            }

            var appointment = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentDate = dto.AppointmentDate,
                Reason = dto.Reason
            };

            _dbContext.Appointments.Add(appointment);
            await _dbContext.SaveChangesAsync();

            return new OperationResultDto
            {
                Success = true,
                Message = "Appointment scheduled successfully."
            };
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsAsync()
        {
            var patientId = GetLoggedInPatientId();
            return await _dbContext.Appointments
                .Where(a => a.PatientId == patientId)
                .Select(a => new AppointmentDto
                {
                    AppointmentId = a.Id,
                    DoctorId = a.Doctor.Id,
                    Date = a.AppointmentDate,
                    Reason = a.Reason
                })
                .ToListAsync();
        }

        public async Task<OperationResultDto> RescheduleAppointmentAsync(int appointmentId, AppointmentRescheduleDto dto)
        {
            var appointment = await _dbContext.Appointments.FindAsync(appointmentId);
            if (appointment == null || appointment.PatientId != GetLoggedInPatientId())
            {
                return new OperationResultDto
                {
                    Success = false,
                    Message = "Appointment not found or unauthorized."
                };
            }

            appointment.AppointmentDate = dto.NewDate;

            await _dbContext.SaveChangesAsync();

            return new OperationResultDto
            {
                Success = true,
                Message = "Appointment rescheduled successfully."
            };
        }

        public async Task<OperationResultDto> CancelAppointmentAsync(int appointmentId)
        {
            var appointment = await _dbContext.Appointments.FindAsync(appointmentId);
            if (appointment == null || appointment.PatientId != GetLoggedInPatientId())
            {
                return new OperationResultDto
                {
                    Success = false,
                    Message = "Appointment not found or unauthorized."
                };
            }

            _dbContext.Appointments.Remove(appointment);
            await _dbContext.SaveChangesAsync();

            return new OperationResultDto
            {
                Success = true,
                Message = "Appointment cancelled successfully."
            };
        }

        public async Task<IEnumerable<AppointmentHistoryDto>> GetAppointmentHistoryAsync()
        {
            var patientId = GetLoggedInPatientId();
            return await _dbContext.Appointments
                .Where(a => a.PatientId == patientId)
                .OrderByDescending(a => a.AppointmentDate)
                .Select(a => new AppointmentHistoryDto
                {
                    AppointmentId = a.Id,
                    DoctorId = a.DoctorId,
                    Date = a.AppointmentDate,
                    Reason = a.Reason,
                })
                .ToListAsync();
        }

        private int GetLoggedInPatientId()
        {
            // Replace with actual logic to retrieve logged-in user's PatientId
            return 1; // Placeholder
        }
    }
}
