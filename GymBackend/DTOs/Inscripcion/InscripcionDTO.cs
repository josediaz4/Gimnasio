using GymBackend.Enums;
using System.Text.Json.Serialization;

namespace GymBackend.DTOs.Inscripcion
{
    public class InscripcionDTO
    {
        public int IdInscripcion { get; set; }
        public int IdSocio { get; set; }
        public string NombreCompletoSocio { get; set; }
        public int IdHorario { get; set; }
        public string NombreActividad { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DiaSemana DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public DateTime FechaInscripcion { get; set; }
    }
}
