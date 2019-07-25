using System;
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
        }
    }
}
