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
    public class SociosController : ControllerBase
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
        public async Task<ActionResult<SocioDTO>> PostSocio(SocioDTO socioDTO)
        {
            var emailExiste = await _contexto.Socios.AnyAsync(s => s.Email == socioDTO.Email);
            if (emailExiste)
                return BadRequest("Ya existe un socio con ese email.");

            var socio = _mapper.Map<Socio>(socioDTO);

            socio.FechaInscripcion = DateTime.UtcNow;
            _contexto.Socios.Add(socio);
            await _contexto.SaveChangesAsync();

            var result = _mapper.Map<SocioDTO>(socio);

            return CreatedAtAction(
                nameof(GetSocio),
                new { id = socio.IdSocio },
                result);
        }

        //PUT: api/socios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSocio(int id, SocioDTO socioDTO)
        {
            if (id != socioDTO.IdSocio)
                return BadRequest("El ID no coincide con el socio enviado.");

            var socioExist = await _contexto.Socios.FindAsync(id);
            if (socioExist == null)
                return NotFound("El socio no existe.");

            _mapper.Map(socioDTO, socioExist);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        //DELETE: api/socios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocio(int id)
        {
            var socio = await _contexto.Socios.FindAsync(id);
            if (socio == null)
                return NotFound("El socio no existe.");

            _contexto.Socios.Remove(socio);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

    }
}
