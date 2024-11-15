using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assesment_DiegoFelipeSalamancaRojas.Models
{
    public class MedicalRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        [Required]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public string? Prescriptions { get; set; }
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}