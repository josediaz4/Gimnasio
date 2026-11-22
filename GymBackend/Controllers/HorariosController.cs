using AutoMapper;
using GymBackend.Data;
using GymBackend.DTOs;
using GymBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorariosController : ControllerBase
    {
        private readonly GimnasioContexto _contexto;
        private readonly IMapper _mapper;
        public HorariosController(GimnasioContexto contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        // GET: api/horarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioDTO>>> GetHorarios()
        {
            var horarios = await _contexto.Horarios.ToListAsync();

            var conteo = await _contexto.Inscripciones
                .GroupBy(i => i.IdHorario)
                .Select(g => new { IdHorario = g.Key, Cantidad = g.Count() })
                .ToDictionaryAsync(x => x.IdHorario, x => x.Cantidad);

            var resultado = horarios
                .OrderBy(h => h.DiaSemana)
                .ThenBy(h => h.HoraInicio)
                .Select(h =>
                {
                    var dto = _mapper.Map<HorarioDTO>(h);
                    dto.Inscriptos = conteo.GetValueOrDefault(h.IdHorario);
                    return dto;
                })
                .ToList();
            return resultado;
        }

        // GET: api/horarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioDTO>> GetHorario(int id)
        {
            var horario = await _contexto.Horarios.FindAsync(id);
            if (horario == null)
                return NotFound("El horario no existe");
            return _mapper.Map<HorarioDTO>(horario);
        }

        // POST: api/horarios
        [HttpPost]
        public async Task<ActionResult<HorarioDTO>> PostHorario([FromBody] HorarioDTO horarioDTO)
        {
            var horario = _mapper.Map<Horario>(horarioDTO);

            _contexto.Horarios.Add(horario);
            await _contexto.SaveChangesAsync();

            var result = _mapper.Map<HorarioDTO>(horario);

            return CreatedAtAction(
                nameof(GetHorario),
                new { id = horario.IdHorario },
                result);
        }

        // PUT: api/horarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorario(int id, [FromBody] HorarioDTO horarioDTO)
        {
            if (id != horarioDTO.IdHorario)
                return BadRequest("El ID no coincide con el horario enviado.");

            var horario = await _contexto.Horarios.FindAsync(id);

            if (horario == null)
                return NotFound("El horario no exíste.");

            var inscriptos = await _contexto.Inscripciones.CountAsync(i => i.IdHorario == id);
            if (horarioDTO.CapacidadMaxima < inscriptos)
                return BadRequest($"La clase ya tiene {inscriptos} inscriptos; la capacidad no puede ser menor.");

            _mapper.Map(horarioDTO, horario);  //<--actualiza las propiedades
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/horarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            var horario = await _contexto.Horarios.FindAsync(id);
            if (horario == null)
                return NotFound("El horario no existe.");

            if (await _contexto.Inscripciones.AnyAsync(i => i.IdHorario == id) || await _contexto.Asistencias.AnyAsync(a => a.IdHorario == id))
                return BadRequest("No se puede eliminar una clase con inscriptos o asistencias registradas.");

            _contexto.Horarios.Remove(horario);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

    }
}
