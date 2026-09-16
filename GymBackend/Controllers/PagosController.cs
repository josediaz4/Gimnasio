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
    public class PagosController : Controller
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
                .Include(s => s.Socio)
                .Include(p => p.Plan)
                .ToListAsync();

            return _mapper.Map<List<PagoDTO>>(pagos);
        }

        //GET: api/pagos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PagoDTO>> GetPago(int id)
        {
            var pago = await _contexto.Pagos
                .Include(s => s.Socio)
                .Include(p => p.Plan)
                .FirstOrDefaultAsync(x => x.IdPago == id);

            if (pago == null) return NotFound();

            return _mapper.Map<PagoDTO>(pago);
        }

        //POST: api/pagos
        [HttpPost]
        public async Task<ActionResult<Pago>> PostPago(Pago pago)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            pago.FechaPago = DateTime.UtcNow;
            _contexto.Pagos.Add(pago);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPago),
                new { id = pago.IdPago },
                pago);
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
