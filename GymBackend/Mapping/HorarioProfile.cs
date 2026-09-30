using AutoMapper;
using GymBackend.DTOs;
using GymBackend.Models;

namespace GymBackend.Mapping
{
    public class HorarioProfile : Profile
    {
        public HorarioProfile()
        {
            CreateMap<HorarioDTO, Horario>().ReverseMap();
        }
    }
}
