namespace GymBackend.DTOs
{
    public class PlanPagoDTO
    {
        public int IdPlan { get; set; }
        public string NombrePlan { get; set; }
        public decimal Precio { get; set; }
        public int CantidadDias { get; set; }
    }
}
