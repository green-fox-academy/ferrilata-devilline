using AutoMapper;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Interfaces;

namespace ferrilata_devilline.Services.Helpers.AutoMapper.Profiles
{
    public class PitchToPitches : Profile
    {
        public PitchToPitches(IPitchService pitchService)
        {
            CreateMap<Pitch, Pitches>();
        }
    }
}
