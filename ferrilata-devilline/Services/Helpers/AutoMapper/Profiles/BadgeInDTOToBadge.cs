using System.Linq;
using AutoMapper;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;

namespace ferrilata_devilline.Services.Helpers.AutoMapper.Profiles
{
    public class BadgeInDTOToBadge : Profile
    {
        public BadgeInDTOToBadge()
        {
            CreateMap<BadgeInDTO, Badge>();
            CreateMap<Badge, BadgeDTO>();
            CreateMap<User, PersonDTO>();

            var levelMap = CreateMap<Level, LevelOutDTO>();
            levelMap.ForMember(x => x.Holders,
                               x => x.MapFrom(y => y.UserLevels.Select(z => z.User)));
        }
    }
}