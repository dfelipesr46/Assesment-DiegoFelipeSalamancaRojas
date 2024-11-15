using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Assesment_DiegoFelipeSalamancaRojas.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string Specialty { get; set; }

        [Required]
        [MaxLength(50)]
        public string LicenseNumber { get; set; }

        public List<Availability> Availabilities { get; set; }

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();

        public List<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>(); 

    }
}