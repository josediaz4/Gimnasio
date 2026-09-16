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
    public class SociosController : Controller
    {
        private readonly GimnasioContexto _contexto;
        private readonly IMapper _mapper;

        public SociosController(GimnasioContexto contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        //GET: api/socios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SocioDTO>>> GetSocios()
        {
            var socios = await _contexto.Socios.ToListAsync();
            return _mapper.Map<List<SocioDTO>>(socios);
        }

        //GET: api/socios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SocioDTO>> GetSocio(int id)
        {
            var socio = await _contexto.Socios.FindAsync(id);
            if (socio == null)
            {
                return NotFound();
            }

            return _mapper.Map<SocioDTO>(socio);
        }

        //POST: api/socios
        [HttpPost]
        public async Task<ActionResult<Socio>> PostSocio(Socio socio)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            socio.FechaInscripcion = DateTime.UtcNow;
            _contexto.Socios.Add(socio);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetSocio),
                new { id = socio.IdSocio },
                socio);
        }

        //PUT: api/socios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSocio(int id, Socio socio)
        {
            if (id != socio.IdSocio)
                return BadRequest("El ID no coincide con el socio enviado.");

            try
            {
                _contexto.Entry(socio).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_contexto.Socios.Any(s => s.IdSocio == id))
                    return NotFound("El socio no existe.");
                else
                    throw;
            }

            return NoContent();
        }

        //DELETE: api/socios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocio(int id)
        {
            var socio = await _contexto.Socios.FindAsync(id);
            if (socio == null)
                return NotFound();

            _contexto.Socios.Remove(socio);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

    }
}
