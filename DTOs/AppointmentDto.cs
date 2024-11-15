using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assesment_DiegoFelipeSalamancaRojas.DTOs
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public string PatientName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
