namespace GymBackend.DTOs
{
    public class PagoDTO
    {
        public int IdPago { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; }
        public DateTime FechaVencimiento { get; set; }

        //datos del socio
        public int IdSocio { get; set; }
        public string NombreCompletoSocio { get; set; }

        //datos del plan
        public int IdPlan { get; set; }
        public string NombrePlan { get; set; }
        public decimal PrecioPlan { get; set; }
    }

}
