using AutoMapper;
using ferrilata_devilline.Services.Helpers.AutoMapper.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace ferrilata_devilline.Services.Helpers.Extensions
{
    public static class AutoMapperSetup
    {
        public static void SetUpAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BadgeTransformer());
                cfg.AddProfile(new PitchToPitches());
            });

            IMapper Mapper = config.CreateMapper();
            services.AddSingleton(Mapper);
        }
    }
}