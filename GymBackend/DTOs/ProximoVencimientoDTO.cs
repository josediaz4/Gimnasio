using GymBackend.Enums;

namespace GymBackend.DTOs
{
    public class ProximoVencimientoDTO
    {
        public int IdSocio { get; set; }
        public string NombreCompleto { get; set; }
        public string NombrePlan { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int DiasRestantes { get; set; }
        public EstadoMembresia EstadoMembresia { get; set; }
    }
}
