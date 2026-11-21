using GymBackend.Data;
using GymBackend.DTOs.Inscripcion;
using GymBackend.Enums;
using GymBackend.Helpers;
using GymBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GymBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InscripcionesController : ControllerBase
    {
        private readonly GimnasioContexto _contexto;

        public InscripcionesController(GimnasioContexto contexto)
        {
            _contexto = contexto;
        }

        private static readonly Expression<Func<Inscripcion, InscripcionDTO>> Proyeccion = i => new InscripcionDTO
        {
            IdInscripcion = i.IdInscripcion,
            IdSocio = i.IdSocio,
            NombreCompletoSocio = i.Socio.Nombre + " " + i.Socio.Apellido,
            IdHorario = i.IdHorario,
            NombreActividad = i.Horario.NombreActividad,
            DiaSemana = i.Horario.DiaSemana,
            HoraInicio = i.Horario.HoraInicio,
            HoraFin = i.Horario.HoraFin,
            FechaInscripcion = i.FechaInscripcion
        };

        // GET: api/inscripciones/horario/3  (inscriptos de una clase)
        [HttpGet("horario/{idHorario}")]
        public async Task<ActionResult<IEnumerable<InscripcionDTO>>> GetPorHorario(int idHorario)
        {
            var horarioExiste = await _contexto.Horarios.AnyAsync(h => h.IdHorario == idHorario);
            if (!horarioExiste)
                return NotFound("La clase no existe.");

            var lista = await _contexto.Inscripciones
                .Where(i => i.IdHorario == idHorario)
                .OrderBy(i => i.Socio.Apellido).ThenBy(i => i.Socio.Nombre)
                .Select(Proyeccion)
                .ToListAsync();

            return Ok(lista);
        }

        // GET: api/inscripciones/socio/5  (clases de un socio)
        [HttpGet("socio/{idSocio}")]
        public async Task<ActionResult<IEnumerable<InscripcionDTO>>> GetPorSocio(int idSocio)
        {
            var socioExiste = await _contexto.Socios.AnyAsync(s => s.IdSocio == idSocio);
            if (!socioExiste)
                return NotFound("Socio no encontrado.");

            var lista = await _contexto.Inscripciones
                .Where(i => i.IdSocio == idSocio)
                .Select(Proyeccion)
                .OrderBy(i => i.DiaSemana).ThenBy(i => i.IdHorario)
                .ToListAsync();

            return Ok(lista);
        }

        // POST: api/inscripciones
        [HttpPost]
        public async Task<ActionResult> Inscribir(CrearInscripcionDTO dto)
        {
            var socio = await _contexto.Socios.FindAsync(dto.IdSocio);
            if (socio == null)
                return NotFound("Socio no encontrado.");

            var horario = await _contexto.Horarios.FindAsync(dto.IdHorario);
            if (horario == null)
                return NotFound("La clase no existe.");

            var ultimoVencimiento = await _contexto.Pagos
                .Where(p => p.IdSocio == dto.IdSocio)
                .MaxAsync(p => (DateTime)p.FechaVencimiento);

            if (MembresiaHelper.Calcular(socio.EstadoMembresia, ultimoVencimiento) != EstadoMembresia.Activo)
                return BadRequest("El socio no tiene una membresia activa.");

            var verificarInscripcionSocio = await _contexto.Inscripciones.AnyAsync(i => i.IdSocio == dto.IdSocio && i.IdHorario == dto.IdHorario);
            if (verificarInscripcionSocio)
                return BadRequest("El socio ya está inscripto en una clase/horario");

            var inscriptos = await _contexto.Inscripciones.CountAsync(i => i.IdHorario == dto.IdHorario);
            if (inscriptos >= horario.CapacidadMaxima)
                return BadRequest("La clase no tiene cupos disponibles.");

            var nuevaInscripcion = new Inscripcion
            {
                IdSocio = dto.IdSocio,
                IdHorario = dto.IdHorario,
                FechaInscripcion = DateTime.UtcNow
            };

            _contexto.Inscripciones.Add(nuevaInscripcion);
            await _contexto.SaveChangesAsync();

            return Ok("Socio inscripto correctamente.");
        }

        // DELETE: api/inscripciones/7
        [HttpDelete("{id}")]
        public async Task<IActionResult> Desincribir(int id)
        {
            var inscripcion = await _contexto.Inscripciones.FindAsync(id);
            if (inscripcion == null)
                return NotFound("La inscripción no existe.");

            _contexto.Inscripciones.Remove(inscripcion);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
