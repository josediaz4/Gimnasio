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
        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de relaciones
            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Socio)
                .WithMany(s => s.Pagos)
                .HasForeignKey(p => p.IdSocio)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Plan)
                .WithMany(pp => pp.Pagos)
                .HasForeignKey(p => p.IdPlan)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Socio>()
                .HasOne(s => s.Usuario)
                .WithOne(u => u.Socio)
                .HasForeignKey<Socio>(s => s.IdUsuario);

            modelBuilder.Entity<Socio>()
                .HasIndex(s => s.DNI)
                .IsUnique();

            modelBuilder.Entity<Socio>()
                .HasIndex(s => s.Email)
                .IsUnique();

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Socio)
                .WithMany(s => s.Asistencias)
                .HasForeignKey(a => a.IdSocio)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Horario>()
                .Property(h => h.DiaSemana)
                .HasConversion<string>()
                .HasMaxLength(15);

            modelBuilder.Entity<Inscripcion>()
                .HasOne(i => i.Socio)
                .WithMany()
                .HasForeignKey(i => i.IdSocio)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inscripcion>()
                .HasOne(i => i.Horario)
                .WithMany()
                .HasForeignKey(i => i.IdHorario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inscripcion>()
                .HasIndex(i => new { i.IdSocio, i.IdHorario })
                .IsUnique();

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Horario)
                .WithMany()
                .HasForeignKey(a => a.IdHorario)
                .OnDelete(DeleteBehavior.Restrict);

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
