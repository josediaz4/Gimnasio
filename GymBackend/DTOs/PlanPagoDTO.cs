using System.ComponentModel.DataAnnotations;

namespace GymBackend.DTOs
{
    public class PlanPagoDTO
    {
        public int IdPlan { get; set; }
        [Required, StringLength(50)]
        public string NombrePlan { get; set; }
        [Range(1000,100000)]
        public decimal Precio { get; set; }
        [Range(1,31)]
        public int CantidadClases { get; set; }
        [Range(1,365)]
        public int DuracionDias { get; set; }
    }
}
