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

            var horario = _contexto.Horarios.FindAsync(id);

            if (horario == null)
                return NotFound("El horario no exíste.");

            await _mapper.Map(horarioDTO, horario);  //<--actualiza las propiedades
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

            _contexto.Horarios.Remove(horario);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

    }
}
