using System.ComponentModel.DataAnnotations;

namespace GymBackend.Models
{
    public class Inscripcion
    {
        [Key]
        public int IdInscripcion { get; set; }
        public int IdSocio { get; set; }
        public int IdHorario { get; set; }
        public DateTime FechaInscripcion { get; set; }

        public Socio Socio { get; set; }
        public Horario Horario { get; set; }
    }
}
