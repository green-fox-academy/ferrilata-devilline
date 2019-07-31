using AutoMapper;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Interfaces;
using System.Linq;

namespace ferrilata_devilline.Services.Helpers.AutoMapper.Profiles
{
    public class LevelProfile : Profile
    {
        public LevelProfile()
        {
            CreateMap<LevelInDTO, Level>();
            CreateMap<Level, LevelInDTO>();
        }
    }
}
