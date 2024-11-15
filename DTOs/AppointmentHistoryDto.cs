using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assesment_DiegoFelipeSalamancaRojas.DTOs
{
    public class AppointmentHistoryDto
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; } 
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty; // Doctor Coments
    }
}
