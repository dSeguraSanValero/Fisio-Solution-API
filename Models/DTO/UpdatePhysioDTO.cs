using System.ComponentModel.DataAnnotations;

namespace FisioSolution.Models;

public class UpdatePhysioDTO
{
    [Required]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "La contraseña debe tener entre 5 y 100 caracteres")]
    public string Password { get; set; }

    [Required]
    public TimeSpan OpeningTime { get; set; }

    [Required]
    public TimeSpan ClosingTime { get; set; }

    [Required]
    public bool Availeable { get; set; }

    [Required]
    [Range(0.1, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal Price { get; set; }
}