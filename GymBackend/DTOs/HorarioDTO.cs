namespace GymBackend.DTOs
{
    public class HorarioDTO
    {
        public int IdHorario { get; set; }
        public string NombreActividad { get; set; }
        public string DiaSemana { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public int CapacidadMaxima { get; set; }
    }
}
