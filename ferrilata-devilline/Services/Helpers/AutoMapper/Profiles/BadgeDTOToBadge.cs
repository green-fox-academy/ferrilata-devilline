using AutoMapper;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Models.DTOs.Input;
using ferrilata_devilline.Services.Interfaces;

namespace ferrilata_devilline.Services.Helpers.AutoMapper.Profiles
{
    public class BadgeDTOToBadge : Profile
    {
        public BadgeDTOToBadge()
        {
            CreateMap<BadgeDTO, Badge>()
                .ForMember(dest => dest.Levels, obj => obj.MapFrom(src => src.Levels));
        }
    }
}