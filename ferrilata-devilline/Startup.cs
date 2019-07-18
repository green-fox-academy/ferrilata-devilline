using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services;
using ferrilata_devilline.Services.Helpers;
using ferrilata_devilline.Services.Interfaces;
using ferrilata_devilline.Services.SlackIntegration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ferrilata_devilline
{
    public class Startup
    {
        private IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection =
                        Configuration.GetSection("Authentication:Google");

                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<ApplicationContext>(builder => builder
                .UseMySQL($"server={Environment.GetEnvironmentVariable("FDHOST")}; " +
                          $"database={Environment.GetEnvironmentVariable("FDDATABASE")}; " +
                          $"user={Environment.GetEnvironmentVariable("FDUSERNAME")};" +
                          $" password={Environment.GetEnvironmentVariable("FDPASSWORD")};"));

            services.Configure<SlackOptions>(Configuration.GetSection("SlackOptions"));
            services.AddScoped<IBadgeRepository, BadgeRepository>();
            services.AddScoped<IBadgeService, BadgeService>();
            services.AddScoped<IPitchService, MockPitchService>();
            services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddScoped<ISlackMessagingService, SlackMessagingService>();

            var currentlyUsedContext = services
                .BuildServiceProvider()
                .GetRequiredService<ApplicationContext>();
            currentlyUsedContext.SeedWithData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvc();
            app.UseAuthentication();
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<ApplicationContext>(builder => builder.UseInMemoryDatabase("InMemory"));
        }

        public void ConfigureTestingServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<ApplicationContext>(builder => builder.UseInMemoryDatabase("InMemory"),
                ServiceLifetime.Singleton);
            services.AddScoped<IBadgeRepository, BadgeRepository>();
            services.AddScoped<IBadgeService, BadgeService>();
            services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddScoped<IPitchService, MockPitchService>();
        }
    }
}