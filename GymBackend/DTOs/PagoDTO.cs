namespace GymBackend.DTOs
{
    public class PagoDTO
    {
        public int IdPago { get; set; }
        public string NombreSocio { get; set; }
        public string NombrePlan { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; }
    }
}
