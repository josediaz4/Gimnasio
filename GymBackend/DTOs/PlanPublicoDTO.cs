using GymBackend.Enums;
using System.Text.Json.Serialization;

namespace GymBackend.DTOs
{
    public class PlanPublicoDTO
    {
        public string NombrePlan { get; set; }
        public decimal Precio { get; set; }
        public int CantidadClases { get; set; }
        public int DuracionDias { get; set; }
    }

    public class HorarioPublicoDTO
    {
        public string NombreActividad { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DiaSemana DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}
