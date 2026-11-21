using System.ComponentModel.DataAnnotations;

namespace GymBackend.Models
{
    public class PlanPago
    {
        [Key]
        public int IdPlan { get; set; }
        [Required(ErrorMessage = "El Nombre del plan es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los {1} caracteres.")]
        public string NombrePlan { get; set; }
        [Range(1000, 100000, ErrorMessage = "El precio debe estar entre {1} y {2}.")]
        public decimal Precio { get; set; }
        [Range(1, 31, ErrorMessage = "El cantidad debe estar entre {1} y {2} clases.")]
        public int CantidadClases { get; set; }
        [Range(1,365)]
        public int DuracionDias { get; set; }

        //Relacion con Pagos
        public ICollection<Pago> Pagos { get; set; }
    }

}
