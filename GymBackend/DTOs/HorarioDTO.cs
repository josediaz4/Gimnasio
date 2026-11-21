using GymBackend.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GymBackend.DTOs
{
    public class HorarioDTO : IValidatableObject
    {
        public int IdHorario { get; set; }
        [Required(ErrorMessage = "El nombre de la actividad es obligatorio.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre {2} y {1} caracteres.")]
        public string NombreActividad { get; set; }

        [EnumDataType(typeof(DiaSemana), ErrorMessage = "El día de la semana no es válido.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DiaSemana DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }

        [Range(1, 100, ErrorMessage = "La capacidad debe estar entre {1} y {2}.")]
        public int CapacidadMaxima { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            var limite = TimeSpan.FromHours(24);

            if (HoraInicio < TimeSpan.Zero || HoraInicio >= limite)
                yield return new ValidationResult("La hora de inicio no es válida.", new[] { nameof(HoraInicio) });

            if (HoraFin < TimeSpan.Zero || HoraFin >= limite)
                yield return new ValidationResult(
                    "La hora de fin no es válida.", new[] { nameof(HoraFin) });

            if (HoraFin <= HoraInicio)
                yield return new ValidationResult("La hora de fin bebe ser mayor que la hora de inicio.", new[] { nameof(HoraFin) });
        }
    }
}
