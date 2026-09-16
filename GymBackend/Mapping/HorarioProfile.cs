using AutoMapper;
using GymBackend.DTOs;
using GymBackend.Models;

namespace GymBackend.Mapping
{
    public class HorarioProfile : Profile
    {
        public HorarioProfile()
        {
            CreateMap<HorarioDTO, Horario>()
                .ForMember(dest => dest.HoraInicio,
                            opt => opt.MapFrom(src => $"{src.HoraInicio:hh\\:mm}"))
                .ForMember(dest => dest.HoraFin,
                            opt => opt.MapFrom(src => $"{src.HoraInicio:hh\\:mm}"));
        }
    }
}
