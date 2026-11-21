using GymBackend.Data;
using GymBackend.DTOs;
using GymBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GymBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly GimnasioContexto _contexto;
        private readonly IConfiguration _config;
        private readonly IPasswordHasher<Usuario> _hasher;

        public AuthController(GimnasioContexto contexto, IConfiguration config, IPasswordHasher<Usuario> hasher)
        {
            _contexto = contexto;
            _config = config;
            _hasher = hasher;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Email == login.Email);

            var resultado = usuario == null
                ? PasswordVerificationResult.Failed
                : _hasher.VerifyHashedPassword(usuario, usuario.ContraseñaHash, login.Password);

            if (resultado == PasswordVerificationResult.Failed)
                return Unauthorized("Credenciales inválidas.");

            return Ok(new
            {
                token = GenerateJwtToken(usuario!),
                usuario = new { email = usuario!.Email, rol = usuario.Rol }
            });
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim("sub", usuario.IdUsuario.ToString()),
                new Claim("name", usuario.Email),
                new Claim("role", usuario.Rol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Método privado para hashear contraseñas
        //private static string HashPassword(string password)
        //{
        //    using var sha256 = SHA256.Create();
        //    var bytes = Encoding.UTF8.GetBytes(password);
        //    var hash = sha256.ComputeHash(bytes);
        //    return Convert.ToBase64String(hash);
        //}
    }
}
