using System.ComponentModel.DataAnnotations;

namespace GymBackend.DTOs
{
    public class PlanPagoDTO
    {
        public int IdPlan { get; set; }
        [Required(ErrorMessage = "El nombre del plan es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los {1} caracteres.")]
        public string NombrePlan { get; set; }
        [Range(1000, 100000, ErrorMessage = "El precio debe estar entre {1} y {2}.")]
        public decimal Precio { get; set; }
        [Range(1, 31, ErrorMessage = "La cantidad de clases debe estar entre {1} y {2}.")]
        public int CantidadClases { get; set; }
        [Range(1, 365, ErrorMessage = "La duración debe estar entre {1} y {2} días.")]
        public int DuracionDias { get; set; }
    }
}
