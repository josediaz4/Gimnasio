using AutoMapper;
using GymBackend.DTOs;
using GymBackend.Models;

namespace GymBackend.Mapping
{
    public class PlanProfile : Profile
    {
        public PlanProfile()
        {
            CreateMap<PlanPago, PlanPagoDTO>();
        }
    }
}
