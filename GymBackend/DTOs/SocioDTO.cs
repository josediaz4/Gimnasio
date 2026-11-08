using GymBackend.Enums;

namespace GymBackend.DTOs
{
    public class SocioDTO
    {
        public int IdSocio { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string DNI { get; set; }
        public EstadoMembresia EstadoMembresia { get; set; }
    }
}
