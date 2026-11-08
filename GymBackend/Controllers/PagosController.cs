using AutoMapper;
using GymBackend.Data;
using GymBackend.DTOs;
using GymBackend.Enums;
using GymBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly GimnasioContexto _contexto;
        private readonly IMapper _mapper;

        public PagosController(GimnasioContexto contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        //GET: api/pagos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagoDTO>>> GetPagos()
        {
            var pagos = await _contexto.Pagos
                .Include(p => p.Socio)
                .Include(p => p.Plan)
                .ToListAsync();

            return _mapper.Map<List<PagoDTO>>(pagos);
        }

        //GET: api/pagos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PagoDTO>> GetPago(int id)
        {
            var pago = await _contexto.Pagos
                .Include(p => p.Socio)
                .Include(p => p.Plan)
                .FirstOrDefaultAsync(x => x.IdPago == id);

            if (pago == null) return NotFound();

            return _mapper.Map<PagoDTO>(pago);
        }

        //POST: api/renovar
        [HttpPost("renovar")]
        public async Task<ActionResult> RenovarMembresia(RenovarMembresiaDTO dto)
        {
            await using var transaction = await _contexto.Database.BeginTransactionAsync();

            try
            {
                var socio = await _contexto.Socios.FirstOrDefaultAsync(s => s.IdSocio == dto.IdSocio);
                if (socio == null)
                    return NotFound("Socio no encontrado.");

                var plan = await _contexto.PlanesPago.FirstOrDefaultAsync(pp => pp.IdPlan == dto.IdPlan);
                if (plan == null)
                    return BadRequest("Plan no encontrado.");

                var pago = new Pago
                {
                    IdSocio = socio.IdSocio,
                    IdPlan = plan.IdPlan,
                    FechaPago = DateTime.UtcNow,
                    FechaVencimiento = DateTime.UtcNow.AddMonths(1),
                    Monto = plan.Precio,
                    MetodoPago = dto.MetodoPago
                };

                _contexto.Pagos.Add(pago);
                socio.EstadoMembresia = EstadoMembresia.Activo;

                await _contexto.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok("Membresía renovada correctamente.");
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        //POST: api/pagos
        [HttpPost]
        public async Task<ActionResult<PagoDTO>> PostPago(PagoDTO pagoDTO)
        {
            var socioExiste = await _contexto.Socios.AnyAsync(s => s.IdSocio == pagoDTO.IdSocio);
            var planExiste = await _contexto.PlanesPago.AnyAsync(p => p.IdPlan == pagoDTO.IdPlan);

            if (!socioExiste || !planExiste)
                return BadRequest("El socio o el plan no existen.");

            var pago = _mapper.Map<Pago>(pagoDTO);
            pago.FechaPago = DateTime.UtcNow;

            _contexto.Pagos.Add(pago);
            await _contexto.SaveChangesAsync();

            var result = _mapper.Map<PagoDTO>(pago);

            return CreatedAtAction(
                nameof(GetPago),
                new { id = pago.IdPago },
                result);
        }

        //DELETE: api/pagos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var pago = await _contexto.Pagos.FindAsync(id);
            if (pago == null) return NotFound();

            _contexto.Pagos.Remove(pago);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
