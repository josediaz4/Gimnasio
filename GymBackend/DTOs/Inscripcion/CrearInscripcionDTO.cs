using System.ComponentModel.DataAnnotations;

namespace GymBackend.DTOs.Inscripcion
{
    public class CrearInscripcionDTO
    {
        [Range(1, int.MaxValue, ErrorMessage ="Debe seleccionar un socio.")]
        public int IdSocio { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una clase.")]
        public int IdHorario { get; set; }
    }
}
