using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Assesment_DiegoFelipeSalamancaRojas.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [MaxLength(15)]
        public string? Phone { get; set; }

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } // "admin", "doctor", "patient"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    }
}