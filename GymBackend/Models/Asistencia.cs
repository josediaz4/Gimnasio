using System.ComponentModel.DataAnnotations;

namespace GymBackend.Models
{
    public class Asistencia
    {
        [Key]
        public int IdAsistencia { get; set; }
        [Required]
        public int IdSocio { get; set; }
        public int? IdHorario { get; set; }
        [Required]
        public DateTime FechaAsistencia { get; set; }
        public Socio Socio { get; set; }
        public Horario? Horario { get; set; }
        public bool AsistenciaValida { get; set; } = true;
    }
}
