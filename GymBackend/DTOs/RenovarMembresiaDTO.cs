using System.ComponentModel.DataAnnotations;

namespace GymBackend.DTOs
{
    public class RenovarMembresiaDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Debe indicar el socio.")]
        public int IdSocio { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un plan.")]
        public int IdPlan { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        [RegularExpression("^(Efectivo|Transferencia)$", ErrorMessage = "El método de pago debe ser Efectivo o Transferencia.")]
        public string MetodoPago { get; set; }
    }

}
