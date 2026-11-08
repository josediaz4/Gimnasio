using GymBackend.Enums;

namespace GymBackend.DTOs
{
    public class MembresiaActualDTO
    {
        public int IdSocio { get; set; }
        public string NombreCompleto { get; set; }
        public EstadoMembresia EstadoMembresia { get; set; }
        public string NombrePlan { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; }
    }

}
