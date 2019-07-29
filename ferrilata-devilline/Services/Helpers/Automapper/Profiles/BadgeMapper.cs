using System;
using System.Linq;
using AutoMapper;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;

namespace ferrilata_devilline.Services.Helpers.AutoMapper.Profiles
{
    public class BadgeMapper : Profile
    {
        public BadgeMapper()
        {
            CreateMap<BadgeInDTO, Badge>();
            CreateMap<LevelInDTO, Level>();
            CreateMap<Badge, BadgeOutDTO>();
            CreateMap<Level, LevelOutDTO>()
                .ForMember(dest => dest.Holders
                , opt => opt.MapFrom(src => src.UserLevels.Select(z => z.User)));
            CreateMap<User, PersonDTO>()
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
