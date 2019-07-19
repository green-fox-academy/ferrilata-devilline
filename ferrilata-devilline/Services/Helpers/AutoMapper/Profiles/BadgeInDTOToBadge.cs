using System.Linq;
using AutoMapper;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;

namespace ferrilata_devilline.Services.Helpers.AutoMapper.Profiles
{
    public class BadgeTransformer : Profile
    {
        public BadgeTransformer()
        {
            CreateMap<BadgeInDTO, Badge>();
            CreateMap<Badge, BadgeDTO>();
            CreateMap<User, PersonDTO>().ForMember(x => x.PersonId, x => x.MapFrom(y => y.UserId));
            CreateMap<Level, LevelOutDTO>().ForMember(x => x.Holders,
                x => x.MapFrom(y => y.UserLevels.Select(z => z.User)));
        }
    }
}