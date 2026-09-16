using System.ComponentModel.DataAnnotations;

namespace GymBackend.Models
{
    public class Socio
    {
        public int IdSocio { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string DNI { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telefono { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public bool EstadoMembresia { get; set; }
    }
}
