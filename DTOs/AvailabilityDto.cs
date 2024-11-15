using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assesment_DiegoFelipeSalamancaRojas.DTOs
{
    public class AvailabilityDto
    {
        public DateTime Date { get; set; }
        public bool IsAvailable { get; set; }
        public int DoctorId { get; internal set; }
        public DateTime EndDate { get; internal set; }
    }
}
