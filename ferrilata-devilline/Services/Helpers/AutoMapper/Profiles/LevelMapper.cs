using AutoMapper;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;


namespace ferrilata_devilline.Services.Helpers.AutoMapper.Profiles
{
    public class LevelMapper : Profile
    {
        public LevelMapper()
        {
            CreateMap<LevelInDTO, Level>();
        }
    }
}
