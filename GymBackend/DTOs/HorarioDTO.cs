namespace GymBackend.DTOs
{
    public class HorarioDTO
    {
        public int IdHorario { get; set; }
        public string NombreActividad { get; set; }
        public string DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int CapacidadMaxima { get; set; }
    }
}
