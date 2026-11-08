using GymBackend.Models;

namespace GymBackend.Data
{
    public class DataSeeder
    {
        public static async Task SeedAsync(GimnasioContexto contexto)
        {
            if (!contexto.PlanesPago.Any())
            {
                contexto.PlanesPago.AddRange(
                    new PlanPago
                    {
                        NombrePlan = "Clase",
                        Precio = 6000,
                        CantidadClases = 1
                    },
                    new PlanPago
                    {
                        NombrePlan = "3 veces por Semana",
                        Precio = 20000,
                        CantidadClases = 12
                    },
                    new PlanPago
                    {
                        NombrePlan = "5 veces por Semana",
                        Precio = 23000,
                        CantidadClases = 20
                    }
                    );
            }
            if (!contexto.Usuarios.Any())
            {
                contexto.Usuarios.Add(
                    new Usuario
                    {
                        Email = "admin@gym.com",
                        ContraseñaHash = "1234",
                        Rol = "Admin"
                    }
                    );
            }
            await contexto.SaveChangesAsync();
        }
    }
}
