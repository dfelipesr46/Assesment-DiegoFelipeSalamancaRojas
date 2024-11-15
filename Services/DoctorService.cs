using Assesment_DiegoFelipeSalamancaRojas.Data;
using Assesment_DiegoFelipeSalamancaRojas.DTOs;
using Assesment_DiegoFelipeSalamancaRojas.Extentions;
using Assesment_DiegoFelipeSalamancaRojas.Interfaces;
using Assesment_DiegoFelipeSalamancaRojas.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace Assesment_DiegoFelipeSalamancaRojas.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationDbContext _context;

        public DoctorService(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Get Appointments
public async Task<IEnumerable<AppointmentDto>> GetAppointmentsAsync(ClaimsPrincipal user, string period)
{
    var doctorId = GetDoctorIdFromClaims(user);

    var appointmentsQuery = _context.Appointments
        .Where(a => a.DoctorId == doctorId)
        .AsQueryable();

    // Aplicando los filtros de periodo
    appointmentsQuery = period switch
    {
        "daily" => appointmentsQuery.Where(a => a.AppointmentDate.Date == DateTime.Now.Date),
        "weekly" => appointmentsQuery.Where(a =>
            a.AppointmentDate >= DateTime.Now.StartOfWeek() && a.AppointmentDate <= DateTime.Now.EndOfWeek()),
        "monthly" => appointmentsQuery.Where(a =>
            a.AppointmentDate.Month == DateTime.Now.Month && a.AppointmentDate.Year == DateTime.Now.Year),
        _ => appointmentsQuery
    };

    // Aquí proyectamos en AppointmentDto, incluyendo datos de Patient de manera explícita
    var appointments = await appointmentsQuery
        .Select(a => new AppointmentDto
        {
            AppointmentId = a.Id,
            PatientName = a.Patient.FirstName,
            Date = a.AppointmentDate,
            Reason = a.Reason
        })
        .ToListAsync();

    return appointments;
}



// 2. Update Availability
public async Task<OperationResultDto> UpdateAvailabilityAsync(ClaimsPrincipal user, AvailabilityDto availabilityDto)
{
    var doctorId = GetDoctorIdFromClaims(user);

    // Buscamos si ya existe una disponibilidad para el doctor en la fecha especificada
    var availability = await _context.Availabilities
        .FirstOrDefaultAsync(a => a.DoctorId == doctorId && a.StartTime.Date == availabilityDto.Date.Date);

    if (availability == null)
    {
        // Si no existe disponibilidad, creamos una nueva entrada en la tabla Availability
        availability = new Availability
        {
            DoctorId = doctorId,
            StartTime = availabilityDto.Date,
            EndTime = availabilityDto.EndDate, // Asegúrate de que AvailabilityDto tenga una propiedad EndDate
            IsAvailable = availabilityDto.IsAvailable
        };

        await _context.Availabilities.AddAsync(availability);
    }
    else
    {
        // Si ya existe disponibilidad, actualizamos la existente
        availability.IsAvailable = availabilityDto.IsAvailable;
        availability.StartTime = availabilityDto.Date;
        availability.EndTime = availabilityDto.EndDate; // Actualizamos también la hora de finalización si es necesario
    }

    // Guardamos los cambios en la base de datos
    await _context.SaveChangesAsync();

    return new OperationResultDto { Success = true, Message = "Availability updated successfully." };
}


        // 3. Search Patients
        public async Task<IEnumerable<PatientDto>> SearchPatientsAsync(string searchTerm)
        {
            var patients = await _context.Patients
                .Where(p => EF.Functions.Like(p.FullName, $"%{searchTerm}%") ||
                            EF.Functions.Like(p.Phone, $"%{searchTerm}%"))
                .Select(p => new PatientDto
                {
                    PatientId = p.Id,
                    FullName = p.FullName,
                    Phone = p.Phone,
                    Age = DateTime.Now.Year - p.DateOfBirth.Year
                })
                .ToListAsync();

            return patients;
        }

        // 4. Get Patient Profile
        public async Task<PatientProfileDto?> GetPatientProfileAsync(int patientId)
        {
            var patient = await _context.Patients
                .Include(p => p.MedicalHistories)
                .FirstOrDefaultAsync(p => p.Id == patientId);

            if (patient == null)
                return null;

            return new PatientProfileDto
            {
                PatientId = patient.Id,
                FullName = patient.FullName,
                Phone = patient.Phone,
                Age = DateTime.Now.Year - patient.DateOfBirth.Year,
                Allergies = patient.Allergies,
                Diseases = patient.Diseases,
                MedicalHistory = patient.MedicalHistories.Select(mh => new MedicalHistoryDto
                {
                    Diagnosis = mh.Diagnosis,
                    Treatment = mh.Treatment,
                    Prescription = mh.Prescription,
                    Date = mh.Date
                }).ToList()
            };
        }

        // Helper Method to Get Doctor ID from Claims
        private int GetDoctorIdFromClaims(ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("User is not authenticated."));
        }
    }
}
