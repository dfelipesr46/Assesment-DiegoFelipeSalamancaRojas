using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assesment_DiegoFelipeSalamancaRojas.DTOs
{
    public class AppointmentRescheduleDto
    {
        public DateTime NewDate { get; set; }
        public TimeSpan NewTime { get; set; }
    }
}
