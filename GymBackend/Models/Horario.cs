using GymBackend.Enums;
using System.ComponentModel.DataAnnotations;

namespace GymBackend.Models
{
    public class Horario
    {
        [Key]
        public int IdHorario { get; set; }
        [Required(ErrorMessage = "El nombre de la actividad es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los {1} caracteres.")]
        public string NombreActividad { get; set; }
        [Required(ErrorMessage = "El dia de la semana es obligatorio.")]
        [RegularExpression("^(Lunes|Martes|Miercoles|Jueves|Viernes)$", ErrorMessage = "El dia debe ser valido.")]
        public DiaSemana DiaSemana { get; set; }
        [Required(ErrorMessage = "La hora de incio es obligatoria.")]
        public TimeSpan HoraInicio { get; set; }
        [Required(ErrorMessage = "La hora de fin es obligatoria.")]
        public TimeSpan HoraFin { get; set; }
        [Range(1, 100, ErrorMessage = "La capacidad debe estar entre {1} y {2}.")]
        public int CapacidadMaxima { get; set; }
    }
}
