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
    public class HorariosController : Controller
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
            return _mapper.Map<List<HorarioDTO>>(horarios);
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
        public async Task<ActionResult<Horario>> PostHorario([FromBody] Horario horario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _contexto.Horarios.Add(horario);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }

            return CreatedAtAction(
                nameof(GetHorario),
                new { id = horario.IdHorario },
                horario);
        }

        // PUT: api/horarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorario(int id, [FromBody] Horario horario)
        {
            if (id != horario.IdHorario)
                return BadRequest("El ID no coincide con el horario enviado.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _contexto.Entry(horario).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_contexto.Horarios.Any(h => h.IdHorario == id))
                    return NotFound("El horario no existe.");
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
            return NoContent();
        }

        // DELETE: api/horarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            var horario = await _contexto.Horarios.FindAsync(id);
            if (horario == null)
                return NotFound("El horario no existe.");
            try
            {
                _contexto.Horarios.Remove(horario);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
            return NoContent();
        }

    }
}
