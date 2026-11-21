using GymBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymBackend.Data
{
    public class DataSeeder
    {
        public static async Task SeedAsync(GimnasioContexto contexto)
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
                var admin = new Usuario
                {
                    Email = "admin@gym.com",
                    Rol = "Administrador"
                };
                admin.ContraseñaHash = hasher.HashPassword(admin, "1234");
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
