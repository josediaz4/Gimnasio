using AutoMapper;
using GymBackend.DTOs;
using GymBackend.Models;

namespace GymBackend.Mapping
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.IdSocio,
                            opt => opt.MapFrom(src => src.Socio != null ? src.Socio.IdSocio : (int?)null))
                .ForMember(dest => dest.NombreCompletoSocio,
                            opt => opt.MapFrom(src => src.Socio != null ? src.Socio.Nombre + " " + src.Socio.Apellido : null));

            CreateMap<UsuarioDTO, Usuario>();
        }
    }
}
