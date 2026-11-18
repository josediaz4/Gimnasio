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
    public class PlanesPagoController : ControllerBase
    {
        private readonly GimnasioContexto _contexto;
        private readonly IMapper _mapper;
        public PlanesPagoController(GimnasioContexto contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        //GET: api/planesPago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanPagoDTO>>> GetPlanesPago()
        {
            var planes = await _contexto.PlanesPago.ToListAsync();
            return _mapper.Map<List<PlanPagoDTO>>(planes);
        }

        //GET: api/planesPago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlanPagoDTO>> GetPlanPago(int id)
        {
            var plan = await _contexto.PlanesPago.FindAsync(id);
            if (plan == null) return NotFound();

            return _mapper.Map<PlanPagoDTO>(plan);
        }

        //POST: api/planesPago
        [HttpPost]
        public async Task<ActionResult<PlanPagoDTO>> PostPlanPago([FromBody] PlanPagoDTO planPagoDTO)
        {
            var planPago = _mapper.Map<PlanPago>(planPagoDTO);

            _contexto.PlanesPago.Add(planPago);
            await _contexto.SaveChangesAsync();

            var result = _mapper.Map<PlanPagoDTO>(planPago);

            return CreatedAtAction(
                nameof(GetPlanPago),
                new { id = planPago.IdPlan },
                result);
        }

        //PUT: api/planesPago/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlanPago(int id, PlanPagoDTO planPagoDTO)
        {
            if (id != planPagoDTO.IdPlan)
                return BadRequest("El ID no coincide con el plan enviado.");

            var planExistente = await _contexto.PlanesPago.FindAsync(id);
            if (planExistente == null)
                return NotFound("El plan no exíste.");

            _mapper.Map(planPagoDTO, planExistente);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        //DELETE: api/planesPago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlanPago(int id)
        {
            var plan = await _contexto.PlanesPago.FindAsync(id);
            if (plan == null) return NotFound();
            if (await _contexto.Pagos.AnyAsync(p => p.IdPlan == id))
                return BadRequest("No se puede eliminar un plan con pagos registrados.");

            _contexto.PlanesPago.Remove(plan);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

    }
}
