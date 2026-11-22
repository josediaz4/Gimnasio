using GymBackend.Data;
using GymBackend.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PublicoController : ControllerBase
    {
        private readonly GimnasioContexto _contexto;

        public PublicoController(GimnasioContexto contexto)
        {
            _contexto = contexto;
        }

        //GET: api/publico/planes
        [HttpGet("planes")]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<IEnumerable<PlanPublicoDTO>>> GetPlanes()
        {
            var planes = await _contexto.PlanesPago
                .OrderBy(p => p.Precio)
                .Select(p => new PlanPublicoDTO
                {
                    NombrePlan = p.NombrePlan,
                    Precio = p.Precio,
                    CantidadClases = p.CantidadClases,
                    DuracionDias = p.DuracionDias
                })
                .ToListAsync();

            return Ok(planes);
        }

        //GET: api/publico/horarios
        [HttpGet("horarios")]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<IEnumerable<HorarioPublicoDTO>>> GetHorarios()
        {
            var horarios = await _contexto.Horarios
                .Select(h => new HorarioPublicoDTO
                {
                    NombreActividad = h.NombreActividad,
                    DiaSemana = h.DiaSemana,
                    HoraInicio = h.HoraInicio,
                    HoraFin = h.HoraFin
                })
                .ToListAsync();

            return Ok(horarios.OrderBy(h => h.DiaSemana).ThenBy(h => h.HoraInicio));
        }
    }
}
