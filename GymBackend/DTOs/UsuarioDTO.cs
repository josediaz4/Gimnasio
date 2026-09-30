namespace GymBackend.DTOs
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }

        //datos del socio asociado
        public int? IdSocio { get; set; }
        public string? NombreCompletoSocio { get; set; }
    }
}
