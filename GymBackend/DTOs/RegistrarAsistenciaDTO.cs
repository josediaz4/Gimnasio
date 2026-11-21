using System.ComponentModel.DataAnnotations;

namespace GymBackend.DTOs
{
    public class RegistrarAsistenciaDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Debe indicar el socio.")]
        public int IdSocio { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe indicar la clase.")]
        public int IdHorario { get; set; }
    }
}
