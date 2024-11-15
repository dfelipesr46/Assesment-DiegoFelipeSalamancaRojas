using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assesment_DiegoFelipeSalamancaRojas.Models
{
    public class Availability
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAvailable { get; set; }
    }

}