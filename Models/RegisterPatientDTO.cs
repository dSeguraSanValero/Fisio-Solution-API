using System.ComponentModel.DataAnnotations;

namespace FisioSolution.Models
{
    public class RegisterPatientDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "El nombre debe tener menos de 100 caracteres")]
        public string Name { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 8, ErrorMessage = "El DNI debe tener entre 8 y 10 dígitos")]
        public string Dni { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "La contraseña debe tener entre 5 y 100 caracteres")]
        public string Password { get; set; }

        private DateTime birthDate;

        [Required]
        public DateTime BirthDate
        {
            get => birthDate.Date;
            set => birthDate = value.Date;
        }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "El peso debe ser mayor que 0")]
        public decimal Weight { get; set; }


        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "La altura debe ser mayor que 0")]
        public decimal Height { get; set; }

        [Required]
        public bool Insurance { get; set; }
    }
}