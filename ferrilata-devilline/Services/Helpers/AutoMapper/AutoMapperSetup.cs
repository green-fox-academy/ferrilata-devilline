using AutoMapper;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Helpers.AutoMapper.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace ferrilata_devilline.Services.Helpers.Extensions
{
    public static class AutoMapperSetup
    {
        public static void SetUpAllAutoMappers(this IServiceCollection services, ApplicationContext context)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new BadgeToBadgeOutDTO(context));
                cfg.AddProfile(new BadgeInDTOToBadge(context));
            });

            var Mapper = config.CreateMapper();
            services.AddSingleton(Mapper);
        }
    }
}
