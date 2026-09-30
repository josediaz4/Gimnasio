using System.ComponentModel.DataAnnotations;

namespace GymBackend.Models
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }
        public int IdSocio { get; set; }
        public int IdPlan { get; set; }
        [Required]
        public DateTime FechaPago { get; set; }
        [Range(1000, 100000, ErrorMessage = "El precio debe estar entre {0} y {1}.")]
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; }

        //Relaciones
        public Socio Socio { get; set; }
        public PlanPago Plan { get; set; }
    }
}
