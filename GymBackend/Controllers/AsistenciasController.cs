using GymBackend.Data;
using GymBackend.DTOs;
using GymBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciasController : ControllerBase
    {
        private readonly GimnasioContexto _contexto;

        public AsistenciasController(GimnasioContexto contexto)
        {
            _contexto = contexto;
        }

        //GET: api/asistencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asistencia>>> GetAsistencias()
        {
            return await _contexto.Asistencias
                .OrderByDescending(a => a.FechaAsistencia)
                .ToListAsync();
        }

        //GET: api/asistencias/socio/1
        public async Task<ActionResult<IEnumerable<AsistenciaDTO>>> GetAsistenciasPorSocio(int idSocio)
        {
            var socioExiste = await _contexto.Socios.AnyAsync(x => x.IdSocio == idSocio);
            if (!socioExiste)
                return NotFound("Socio no encontrado.");

            var asistencias = await _contexto.Asistencias
                            .Where(a => a.IdSocio == idSocio)
                            .OrderByDescending(a => a.FechaAsistencia)
                            .Select(a => new AsistenciaDTO
                            {
                                IdAsistencia = a.IdAsistencia,
                                FechaAsistencia = a.FechaAsistencia,
                                AsistenciaValida = a.AsistenciaValida
                            }).ToListAsync();

            return Ok(asistencias);
        }

        //GET: api/asistencias/resumen/1
        [HttpGet("resumen/{idSocio}")]
        public async Task<ActionResult<ResumenAsistenciaDTO>> GetResumen(int idSocio)
        {
            var socio = await _contexto.Socios.FirstOrDefaultAsync(s => s.IdSocio == idSocio);

            if (socio == null)
                return NotFound();

            var ultimoPago = await _contexto.Pagos
                .Include(p => p.Plan)
                .Where(p => p.IdSocio == idSocio)
                .OrderByDescending(p => p.FechaVencimiento)
                .FirstOrDefaultAsync();

            if (ultimoPago == null)
                return BadRequest("El socio no posee pagos.");

            var utilizadas = await _contexto.Asistencias.CountAsync(a => a.IdSocio == idSocio && a.AsistenciaValida && a.FechaAsistencia >= ultimoPago.FechaPago);

            var permitidas = ultimoPago.Plan.CantidadClases;

            var resultado = new ResumenAsistenciaDTO
            {
                IdSocio = idSocio,
                NombreCompleto = $"{socio.Nombre} {socio.Apellido}",
                ClasesPermitidas = permitidas,
                ClasesUtilizadas = utilizadas,
                ClasesRestantes = permitidas - utilizadas
            };

            return Ok(resultado);
        }

        //POST: api/asistencias
        [HttpPost]
        public async Task<ActionResult> RegistrarAsistencias(RegistrarAsistenciaDTO dto)
        {
            var socio = await _contexto.Socios.FirstOrDefaultAsync(s => s.IdSocio == dto.IdSocio);
            if (socio == null)
                return NotFound("Socio no encontrado.");

            var ultimoPago = await _contexto.Pagos
                .Include(p => p.Plan)
                .Where(p => p.IdSocio == dto.IdSocio)
                .OrderByDescending(p => p.FechaVencimiento)
                .FirstOrDefaultAsync();
            if (ultimoPago == null)
                return BadRequest("El socio no tiene datos registrados.");
            if (ultimoPago.FechaVencimiento.Date < DateTime.UtcNow.Date)
                return BadRequest("La membresía está vencida.");

            var asistenciasUtilizadas = await _contexto.Asistencias
                                        .CountAsync(a => a.IdSocio == dto.IdSocio && a.AsistenciaValida && a.FechaAsistencia >= ultimoPago.FechaPago);

            var clasesPermitidas = ultimoPago.Plan.CantidadClases;

            if (asistenciasUtilizadas >= clasesPermitidas)
                return BadRequest("No posee clases disponibles.");

            var asistencia = new Asistencia
            {
                IdSocio = dto.IdSocio,
                FechaAsistencia = DateTime.UtcNow,
                AsistenciaValida = true
            };

            _contexto.Asistencias.Add(asistencia);
            await _contexto.SaveChangesAsync();

            return Ok("Asistencia registada correctamente.");
        }

        //PUT: api/asistencias/5
        [HttpPut("{id}/invalidar")]
        public async Task<ActionResult> InvalidarAsistencia(int id)
        {
            var asistencia = await _contexto.Asistencias
                .FirstOrDefaultAsync(a => a.IdAsistencia == id);

            if (asistencia == null)
                return NotFound();

            asistencia.AsistenciaValida = false;
            await _contexto.SaveChangesAsync();

            return Ok("Asistencia invalidada.");
        }
    }
}
