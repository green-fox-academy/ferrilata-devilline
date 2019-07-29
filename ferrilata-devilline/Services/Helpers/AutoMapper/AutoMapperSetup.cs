using AutoMapper;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Helpers.AutoMapper.Profiles;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ferrilata_devilline.Services.Helpers.Extensions
{
    public static class AutoMapperSetup
    {
        public static void SetUpAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new BadgeInDTOToBadge());
                cfg.AddProfile(new PitchToPitches());
                cfg.AddProfile(new LevelMapper());
            });

            IMapper Mapper = config.CreateMapper();
            services.AddSingleton(Mapper);
        }
    }
}
