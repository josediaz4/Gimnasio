using AutoMapper;
using GymBackend.DTOs;
using GymBackend.Models;

namespace GymBackend.Mapping
{
    public class PagoProfile : Profile
    {
        public PagoProfile()
        {
            CreateMap<Pago, PagoDTO>()
                .ForMember(dest => dest.NombreCompletoSocio,
                            opt => opt.MapFrom(src => src.Socio.Nombre + " " + src.Socio.Apellido))
                .ForMember(dest => dest.NombrePlan,
                            opt => opt.MapFrom(src => src.Plan.NombrePlan))
                .ForMember(dest => dest.PrecioPlan,
                            opt => opt.MapFrom(src => src.Plan.Precio));
            CreateMap<PagoDTO, Pago>();
        }
    }
}
