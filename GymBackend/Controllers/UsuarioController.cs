using AutoMapper;
using GymBackend.Data;
using GymBackend.DTOs;
using GymBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly GimnasioContexto _contexto;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Usuario> _hasher;

        public UsuarioController(GimnasioContexto contexto, IMapper mapper, IPasswordHasher<Usuario> hasher)
        {
            _contexto = contexto;
            _mapper = mapper;
            _hasher = hasher;
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
                return NotFound("El usuario no existe.");

            return _mapper.Map<UsuarioDTO>(usuario);
        }

        //POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> PostUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            var emailExiste = await _contexto.Usuarios.AnyAsync(u => u.Email == usuarioDTO.Email);
            if (emailExiste)
                return BadRequest("Ya existe un usuario con este email.");


            if (string.IsNullOrWhiteSpace(usuarioDTO.Password) || usuarioDTO.Password.Length < 6)
                return BadRequest("La contraseña debe tener al menos 6 caracteres.");

            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            usuario.ContraseñaHash = _hasher.HashPassword(usuario, usuarioDTO.Password);

            _contexto.Usuarios.Add(usuario);
            await _contexto.SaveChangesAsync();

            var result = _mapper.Map<UsuarioDTO>(usuario);

            return CreatedAtAction(
                nameof(GetUsuario),
                new { id = usuario.IdUsuario },
                result
                );
        }

        //PUT: api/usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] UsuarioDTO usuarioDTO)
        {
            if (id != usuarioDTO.IdUsuario)
                return BadRequest("El Id no coincide con el usuario");

            var usuarioExiste = await _contexto.Usuarios.FindAsync(id);
            if (usuarioExiste == null)
                return NotFound("El usuario no existe.");

            _mapper.Map(usuarioDTO, usuarioExiste);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        //DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound("El usuario no existe.");

            _contexto.Usuarios.Remove(usuario);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        ////////////////////////////////
        //////// METODOS//////////////// 
        ////////////////////////////////

        //Crear Usuarios
        [HttpPost("crear-usuario/{idSocio}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<UsuarioDTO>> CrearUsuarioDesdeSocio(int idSocio)
        {
            var socio = await _contexto.Socios.FindAsync(idSocio);
            if (socio == null)
                return NotFound("El socio no existe.");

            //Validar que no tenga un usuario asignado
            if (socio.IdUsuario != null)
                return BadRequest("Este socio ya tiene un usuario registrado.");

            //Crear usuario a partir de datos del socio
            var usuario = new Usuario
            {
                Email = socio.Email,
                Rol = "Socio"
            };
            usuario.ContraseñaHash = _hasher.HashPassword(usuario, socio.DNI);

            _contexto.Usuarios.Add(usuario);
            await _contexto.SaveChangesAsync();

            //vincular socio con usuario
            socio.IdUsuario = usuario.IdUsuario;
            socio.Usuario = usuario;
            await _contexto.SaveChangesAsync();

            var result = _mapper.Map<UsuarioDTO>(usuario);
            return CreatedAtAction(
                nameof(GetUsuario),
                new { id = usuario.IdUsuario },
                result);
        }

        //Cambiar contraseña
        [HttpPut("cambiar-password/{idUsuario}")]
        public async Task<IActionResult> CambiarPassword(int idUsuario, [FromBody] string nuevaPass)
        {
            var usuario = await _contexto.Usuarios.FindAsync(idUsuario);
            if (usuario == null)
                return NotFound("El usuario no existe.");

            if (string.IsNullOrWhiteSpace(nuevaPass) || nuevaPass.Length < 6)
                return BadRequest("La contraseña debe tener al menos 6 caracteres.");

            usuario.ContraseñaHash = _hasher.HashPassword(usuario, nuevaPass);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
