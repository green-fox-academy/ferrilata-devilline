using AutoMapper;
using ferrilata_devilline.Services.Helpers.AutoMapper.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace ferrilata_devilline.Services.Helpers.AutoMapper
{
    public static class AutoMapperSetup
    {
        public static void SetUpAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BadgeProfile());
                cfg.AddProfile(new PitchProfile());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}