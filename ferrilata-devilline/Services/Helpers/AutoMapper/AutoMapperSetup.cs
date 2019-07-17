using AutoMapper;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Helpers.AutoMapper.Profiles;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ferrilata_devilline.Services.Helpers.Extensions
{
    public static class AutoMapperSetup
    {
        public static void SetUpAllAutoMappers(this IServiceCollection services)
        {
            var badgeService = services
                .BuildServiceProvider()
                .GetRequiredService<IBadgeService>();
            var pitchService = services
                .BuildServiceProvider()
                .GetRequiredService<IPitchService>();

            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new BadgeInDTOToBadge(badgeService));
                cfg.AddProfile(new PitchToPitches(pitchService));
            });

            var Mapper = config.CreateMapper();
            services.AddSingleton(Mapper);
        }
    }
}
