using AutoMapper;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Repositories;

namespace ferrilata_devilline.Services.Helpers.AutoMapper.Profiles
{
    public class BadgeToBadgeOutDTO : Profile
    {
        public BadgeToBadgeOutDTO(ApplicationContext context)
        {
            CreateMap<Badge, BadgeOutDTO>();
        }
    }
}
