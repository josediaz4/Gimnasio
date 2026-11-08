namespace GymBackend.DTOs
{
    public class ResumenAsistenciaDTO
    {
        public int IdSocio { get; set; }
        public string NombreCompleto { get; set; }
        public int ClasesPermitidas { get; set; }
        public int ClasesUtilizadas { get; set; }
        public int ClasesRestantes { get; set; }
    }
}
