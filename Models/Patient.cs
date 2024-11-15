using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assesment_DiegoFelipeSalamancaRojas.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Allergies { get; set; }
        public string? ChronicDiseases { get; set; }
        public List<MedicalRecord> MedicalRecords { get; set; } 
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();

    }
}