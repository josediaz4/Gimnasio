using GymBackend.Enums;

namespace GymBackend.DTOs.Socio
{
    public class SocioDTO
    {
        public int IdSocio { get; set; }
        public string NombreCompleto { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string DNI { get; set; }
        public string Telefono { get; set; }
        public EstadoMembresia EstadoMembresia { get; set; }
    }
}
