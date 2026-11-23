using GymBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymBackend.Data
{
    public class DataSeeder
    {
        public static async Task SeedAsync(GimnasioContexto contexto, IConfiguration config)
        {
            var hasher = new PasswordHasher<Usuario>();

            if (!contexto.PlanesPago.Any())
            {
                contexto.PlanesPago.AddRange(
                    new PlanPago
                    {
                        NombrePlan = "Clase",
                        Precio = 6000,
                        CantidadClases = 1,
                        DuracionDias = 1
                    },
                    new PlanPago
                    {
                        NombrePlan = "3 veces por Semana",
                        Precio = 20000,
                        CantidadClases = 12,
                        DuracionDias = 30
                    },
                    new PlanPago
                    {
                        NombrePlan = "5 veces por Semana",
                        Precio = 23000,
                        CantidadClases = 20,
                        DuracionDias = 30
                    }
                    );
            }
            if (!contexto.Usuarios.Any())
            {
                var email = config["Admin:Email"] ?? "admin@gym.com";
                var password = config["Admin:Password"];

                if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                    throw new InvalidOperationException(
                        "Configurá Admin:Password (mínimo 8 caracteres) para crear el administrador inicial.");

                var admin = new Usuario { Email = email, Rol = "Administrador" };
                admin.ContraseñaHash = hasher.HashPassword(admin, password);
                contexto.Usuarios.Add(admin);
            }
            await contexto.SaveChangesAsync();

            // Migración única: pasa a hash las contraseñas que seguían en texto plano
            // (un hash mide 84 caracteres; el texto plano, mucho menos)
            var sinHash = await contexto.Usuarios
                .Where(u => u.ContraseñaHash.Length < 60)
                .ToListAsync();

            foreach (var u in sinHash)
            {
                u.ContraseñaHash = hasher.HashPassword(u, u.ContraseñaHash);
            }
            await contexto.SaveChangesAsync();
        }
    }
}
