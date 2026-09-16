using AutoMapper;
using GymBackend.DTOs;
using GymBackend.Models;

namespace GymBackend.Mapping
{
    public class SocioProfile : Profile
    {
        public SocioProfile()
        {
            CreateMap<Socio, SocioDTO>()
                .ForMember(dest => dest.NombreCompleto,
                            opt => opt.MapFrom(src => src.Nombre + " " + src.Apellido));
        }
    }
}
