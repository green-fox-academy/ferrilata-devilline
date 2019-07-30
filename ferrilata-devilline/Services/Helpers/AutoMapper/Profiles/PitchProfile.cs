using AutoMapper;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;

namespace ferrilata_devilline.Services.Helpers.AutoMapper.Profiles
{
    public class PitchProfile : Profile
    {
        public PitchProfile()
        {
            CreateMap<Pitch, Pitches>();
        }
    }
}
