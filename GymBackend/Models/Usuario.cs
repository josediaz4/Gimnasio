using System.ComponentModel.DataAnnotations;

namespace GymBackend.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(256, ErrorMessage = "La contraseña no puede superar los 256 caracteres.")]
        public string ContraseñaHash { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        [RegularExpression("^(Administrador|Socio)$", ErrorMessage = "El rol debe ser Administrador o Socio.")]
        public string Rol { get; set; }
    }
}
