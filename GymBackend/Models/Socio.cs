using System.ComponentModel.DataAnnotations;

namespace GymBackend.Models
{
    public class Socio
    {
        [Key]
        public int IdSocio { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string DNI { get; set; }
        [Required]
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public bool EstadoMembresia { get; set; }

        // Relación con Usuario
        public int? IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        // Relación con Pagos
        public ICollection<Pago> Pagos { get; set; }
    }
}
