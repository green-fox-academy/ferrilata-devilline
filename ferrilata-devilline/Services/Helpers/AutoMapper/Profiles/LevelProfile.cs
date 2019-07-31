using AutoMapper;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using System.Linq;

namespace ferrilata_devilline.Services.Helpers.AutoMapper.Profiles
{
    public class LevelProfile : Profile
    {
        public LevelProfile()
        {
            CreateMap<LevelInDTO, Level>();
            CreateMap<BadgeInDTO, Badge>();
            CreateMap<Badge, BadgeDTO>();
            CreateMap<User, PersonDTO>().ForMember(person => person.PersonId, x => x.MapFrom(src => src.UserId));
            CreateMap<Level, LevelOutDTO>().ForMember(dest => dest.Holders, x => x.MapFrom(src => src.UserLevels.Select(userLevel => userLevel.User)));
        }
    }
}
