using GymBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GymBackend.Data
{
    public class GimnasioContexto: DbContext
    {
        public GimnasioContexto(DbContextOptions<GimnasioContexto> options) : base(options)
        {

        }

        public DbSet<Socio> Socios { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<PlanPago> PlanesPago { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
