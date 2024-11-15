using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assesment_DiegoFelipeSalamancaRojas.DTOs
{
    public class PatientProfileDto
    {
        public int PatientId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Allergies { get; set; } = string.Empty;
        public string Diseases { get; set; } = string.Empty;

        public IEnumerable<MedicalHistoryDto> MedicalHistory { get; set; } = new List<MedicalHistoryDto>();
    }

    public class MedicalHistoryDto
    {
        public string Allergies { get; set; } = string.Empty;
        public string ChronicDiseases { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
