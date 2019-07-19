using AutoMapper;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs.Input;
using System.Collections.Generic;

namespace ferrilata_devilline.Services.Helpers.AutoMapper.Profiles
{
    public class LevelWithoutHoldersToLevel : Profile
    {
        public LevelWithoutHoldersToLevel()
        {
            CreateMap<LevelWithoutHoldersDTO, Level>()
                 .ForMember(dest => dest.UserLevels, opt => opt.MapFrom(a => new List<UserLevel> { })) // if i had access to the database, i could solve these for the first try
                 .ForMember(dest => dest.Pitches, opt => opt.MapFrom(a => new List<Pitch> { }))
                 .ForMember(dest => dest.Badge, opt => opt.MapFrom(a => new Badge { }));
        }
    }
}
