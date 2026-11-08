namespace GymBackend.DTOs
{
    public class CrearSocioDTO
    {
        //Datos del socio
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        //Membresia inicial
        public int IdPlan { get; set; }
        public string MetodoPago { get; set; }
    }
}
