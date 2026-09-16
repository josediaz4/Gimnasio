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
    public class PlanesPagoController : Controller
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
        public async Task<ActionResult<PlanPago>> PostPlanPago([FromBody]PlanPago plan)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _contexto.PlanesPago.Add(plan);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPlanPago),
                new { id = plan.IdPlan },
                plan);
        }

        //PUT: api/planesPago/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlanPago(int id, PlanPago plan)
        {
            if (id != plan.IdPlan) 
                return BadRequest("El ID no coincide con el plan enviado.");

            try
            {
                _contexto.Entry(plan).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_contexto.PlanesPago.Any(p => p.IdPlan == id))
                    return NotFound("El Plan no existe.");
                else
                    throw; //dejamos que el sist maneje el error si es algo mas grabe
            }            

            return NoContent();
        }

        //DELETE: api/planesPago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlanPago(int id)
        {
            var plan = await _contexto.PlanesPago.FindAsync(id);
            if (plan == null) return NotFound();

            _contexto.PlanesPago.Remove(plan);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }

    }
}
