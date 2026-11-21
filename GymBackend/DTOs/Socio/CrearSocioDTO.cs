using System.ComponentModel.DataAnnotations;

namespace GymBackend.DTOs.Socio
{
    public class CrearSocioDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre {2} y {1} caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre {2} y {1} caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [RegularExpression(@"^\d{7,8}$", ErrorMessage = "El DNI debe tener 7 u 8 dígitos, sin puntos.")]
        public string DNI { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        [StringLength(100, ErrorMessage = "El email no puede superar los {1} caracteres.")]
        public string Email { get; set; }

        [RegularExpression(@"^[0-9+\-\s()]{6,20}$", ErrorMessage = "El teléfono no es válido.")]
        public string? Telefono { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un plan.")]
        public int IdPlan { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        [RegularExpression("^(Efectivo|Transferencia)$", ErrorMessage = "El método de pago debe ser Efectivo o Transferencia.")]
        public string MetodoPago { get; set; }
    }
}
