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
    public class UsuarioController : Controller
    {
        private readonly GimnasioContexto _contexto;
        private readonly IMapper _mapper;

        public UsuarioController(GimnasioContexto contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        //GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            var usuarios = await _contexto.Usuarios.ToListAsync();
            return _mapper.Map<List<UsuarioDTO>>(usuarios);
        }

        //GET: api/usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound("El usuario no exíste.");

            return _mapper.Map<UsuarioDTO>(usuario);
        }

        //POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _contexto.Usuarios.Add(usuario);
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }

            return CreatedAtAction(
                nameof(GetUsuario),
                new { id = usuario.IdUsuario },
                usuario
                );
        }

        //PUT: api/usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
                return BadRequest("El Id no coincide con el usuario");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _contexto.Entry(usuario).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_contexto.Usuarios.Any(u => u.IdUsuario == id))
                    return NotFound("El usuario no existe.");
                else
                    throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }

            return NoContent();
        }

        //DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound("El usuario no existe.");

            try
            {
                _contexto.Usuarios.Remove(usuario);
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
