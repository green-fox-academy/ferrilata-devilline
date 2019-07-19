using AutoMapper;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Helpers.AutoMapper.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace ferrilata_devilline.Services.Helpers.Extensions
{
    public static class AutoMapperSetup
    {
        public static void SetUpAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new BadgeDTOToBadge());
                cfg.AddProfile(new PitchToPitches());
                cfg.AddProfile(new LevelWithoutHoldersToLevel());
            });

            IMapper mapper = config.CreateMapper();
            var executionPlan = config.BuildExecutionPlan(typeof(BadgeDTO), typeof(Badge));
            services.AddSingleton<IMapper>(mapper);
        }
    }
}
