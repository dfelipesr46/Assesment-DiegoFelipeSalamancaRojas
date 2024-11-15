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
        [MaxLength(100)]
        public string Specialty { get; set; }

        [Required]
        [MaxLength(50)]
        public string LicenseNumber { get; set; }

        public string Availability { get; set; } // JSON for dynamic availability
    }
}