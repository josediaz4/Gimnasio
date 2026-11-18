using AutoMapper;
using GymBackend.Data;
using GymBackend.DTOs;
using GymBackend.DTOs.Socio;
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

        //GET: api/socios/5/historial-pagos
        [HttpGet("{id}/historial-pagos")]
        public async Task<ActionResult<IEnumerable<PagoDTO>>> GetHistorialPagos(int id)
        {
            var socioExiste = await _contexto.Socios.AnyAsync(s => s.IdSocio == id);
            if (!socioExiste)
                return NotFound("El socio no existe.");

            var pagos = await _contexto.Pagos
                .Include(p => p.Socio)
                .Include(p => p.Plan)
                .Where(p => p.IdSocio == id)
                .OrderByDescending(p => p.FechaPago)
                .ToListAsync();

            return Ok(_mapper.Map<List<PagoDTO>>(pagos));
        }

        //GET: api/socios/5/membresia-actual
        [HttpGet("{id}/membresia-actual")]
        public async Task<ActionResult<MembresiaActualDTO>> GetMembresiaActual(int id)
        {
            var socio = await _contexto.Socios.FirstOrDefaultAsync(s => s.IdSocio == id);
            if (socio == null)
                return NotFound("Socio no encontrado.");

            var ultimoPago = await _contexto.Pagos
                .Include(p => p.Plan)
                .Where(p => p.IdSocio == id)
                .OrderByDescending(p => p.FechaPago)
                .FirstOrDefaultAsync();

            if (ultimoPago == null)
                return BadRequest("El socio no posee pagos registrados.");

            var resultado = new MembresiaActualDTO
            {
                IdSocio = socio.IdSocio,
                NombreCompleto = $"{socio.Nombre} {socio.Apellido}",
                EstadoMembresia = socio.EstadoMembresia,
                NombrePlan = ultimoPago.Plan.NombrePlan,
                FechaPago = ultimoPago.FechaPago,
                FechaVencimiento = ultimoPago.FechaVencimiento,
                Monto = ultimoPago.Monto,
                MetodoPago = ultimoPago.MetodoPago
            };

            return Ok(resultado);
        }

        //GET: api/socios/proximos-vencimientos
        [HttpGet("proximos-vencimientos")]
        public async Task<ActionResult<IEnumerable<ProximoVencimientoDTO>>> GetProximosVencimientos()
        {
            var hoy = DateTime.UtcNow.Date;
            var fechaLimite = DateTime.UtcNow.AddDays(7);

            var pagos = await _contexto.Pagos
                .Include(p => p.Socio)
                .Include(p => p.Plan)
                .ToListAsync();

            var resultado = pagos
                .GroupBy(p => p.IdSocio)
                .Select(g => g
                           .OrderByDescending(p => p.FechaVencimiento)
                           .First())
                .Where(p => p.FechaVencimiento.Date >= hoy &&
                           p.FechaVencimiento.Date <= fechaLimite)
                .OrderBy(p => p.FechaVencimiento)
                .Select(p => new ProximoVencimientoDTO
                {
                    IdSocio = p.IdSocio,
                    NombreCompleto = $"{p.Socio.Nombre} {p.Socio.Apellido}",
                    NombrePlan = p.Plan.NombrePlan,
                    FechaVencimiento = p.FechaVencimiento,
                    EstadoMembresia = p.Socio.EstadoMembresia,
                    DiasRestantes = (p.FechaVencimiento.Date - hoy).Days
                })
                .ToList();

            return Ok(resultado);
        }

        //POST: api/socios
        [HttpPost]
        public async Task<ActionResult<SocioDTO>> PostSocio(CrearSocioDTO socioDTO)
        {
            await using var transaction = await _contexto.Database.BeginTransactionAsync();
            try
            {
                //Validaciones
                var emailExiste = await _contexto.Socios.AnyAsync(s => s.Email == socioDTO.Email);
                if (emailExiste)
                    return BadRequest("Ya existe un socio con ese email.");

                var dniExiste = await _contexto.Socios.AnyAsync(s => s.DNI == socioDTO.DNI);
                if (dniExiste)
                    return BadRequest("Ya existe un socio con ese DNI.");

                var plan = await _contexto.PlanesPago.FirstOrDefaultAsync(p => p.IdPlan == socioDTO.IdPlan);

                if (plan == null)
                    return BadRequest("El plan seleccionado no existe.");
                //Crear socio
                var socio = _mapper.Map<Socio>(socioDTO);

                socio.FechaInscripcion = DateTime.UtcNow;
                socio.EstadoMembresia = Enums.EstadoMembresia.Activo;

                _contexto.Socios.Add(socio);
                await _contexto.SaveChangesAsync();
                //Crear primer pago
                var pago = new Pago
                {
                    IdSocio = socio.IdSocio,
                    IdPlan = plan.IdPlan,
                    FechaPago = DateTime.UtcNow,
                    FechaVencimiento = DateTime.UtcNow.AddDays(plan.DuracionDias),
                    Monto = plan.Precio,
                    MetodoPago = socioDTO.MetodoPago
                };
                _contexto.Pagos.Add(pago);
                await _contexto.SaveChangesAsync();

                await transaction.CommitAsync();

                var result = _mapper.Map<SocioDTO>(socio);

                return CreatedAtAction(
                    nameof(GetSocio),
                    new { id = socio.IdSocio },
                    result);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

        }

        //PUT: api/socios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSocio(int id, ActualizarSocioDTO dto)
        {
            var socioExist = await _contexto.Socios.FindAsync(id);
            if (socioExist == null)
                return NotFound("El socio no existe.");

            if (await _contexto.Socios.AnyAsync(s => s.IdSocio != id && s.Email == dto.Email))
                return BadRequest("Ya existe un socio con ese email");
            if (await _contexto.Socios.AnyAsync(s => s.IdSocio != id && s.DNI == dto.DNI))
                return BadRequest("Ya existe un socio con ese DNI.");


            socioExist.Nombre = dto.Nombre;
            socioExist.Apellido = dto.Apellido;
            socioExist.DNI = dto.DNI;
            socioExist.Email = dto.Email;
            socioExist.Telefono = dto.Telefono ?? "";

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
