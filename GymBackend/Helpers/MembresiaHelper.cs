using GymBackend.Enums;

namespace GymBackend.Helpers
{
    public static class MembresiaHelper
    {
        public static EstadoMembresia Calcular(EstadoMembresia almacenado, DateTime? ultimoVencimiento)
        {
            if (almacenado == EstadoMembresia.Suspendido)
                return EstadoMembresia.Suspendido;

            var vigente = ultimoVencimiento.HasValue && ultimoVencimiento.Value.Date >= DateTime.UtcNow.Date;

            return vigente ? EstadoMembresia.Activo : EstadoMembresia.Vencido;
        }
    }
}
