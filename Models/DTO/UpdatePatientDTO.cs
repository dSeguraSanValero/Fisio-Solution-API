using System.ComponentModel.DataAnnotations;

namespace FisioSolution.Models;

public class UpdatePatientDTO
{
    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres")]
    public string? Password { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "El peso debe ser mayor que 0")]
    public decimal Weight { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "La altura debe ser mayor que 0")]
    public decimal Height { get; set; }

    [Required]
    public bool Insurance { get; set; }
}