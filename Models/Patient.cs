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

        [Required]
        public int Age { get; set; }

        public string? Allergies { get; set; }
        public string? ChronicDiseases { get; set; }
    }
}