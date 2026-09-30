using GymBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace GymBackend.Data
{
    public class GimnasioContexto : DbContext
    {
        public GimnasioContexto(DbContextOptions<GimnasioContexto> options) : base(options)
        {

        }

        public DbSet<Socio> Socios { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<PlanPago> PlanesPago { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de relaciones
            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Socio)
                .WithMany(s => s.Pagos)
                .HasForeignKey(p => p.IdSocio);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Plan)
                .WithMany(pp => pp.Pagos)
                .HasForeignKey(p => p.IdPlan);

            modelBuilder.Entity<Socio>()
                .HasOne(s => s.Usuario)
                .WithOne(u => u.Socio)
                .HasForeignKey<Socio>(s => s.IdUsuario);

            // Precision para DECIMAL
            modelBuilder.Entity<Pago>()
                .Property(p => p.Monto)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PlanPago>()
                .Property(pp => pp.Precio)
                .HasPrecision(10, 2);
        }
    }
}
