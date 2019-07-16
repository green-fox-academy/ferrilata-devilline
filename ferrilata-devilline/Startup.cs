using Microsoft.Extensions.Configuration;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

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
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("TOKENSECRET"));

            services.AddAuthentication(options =>
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection =
                        Configuration.GetSection("Authentication:Google");

                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateLifetime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    //options.Events.AuthenticationFailed(context: )
                    //Events = new JwtBearerEvents()
                    //{
                    //    OnAuthenticationFailed = context =>
                    //    {
                    //        context.Response.OnStarting(async () =>
                    //        {
                    //            context.Response.ContentType = "text/plain";
                    //            await context.Response.WriteAsync(context.Exception.Message);
                    //        });

                    //        return Task.CompletedTask;
                    //    }
                    //};
                })
                ;
            ;





            services.AddMvc();
                // All endpoints need authorization using our custom authorization filter
                

            services.AddScoped<IBadgeService, MockBadgeService>();
            services.AddScoped<IPitchService, MockPitchService>();

            services.AddDbContext<ApplicationContext>(builder => builder


            .UseMySQL($"server={Environment.GetEnvironmentVariable("FDHOST", EnvironmentVariableTarget.User)}; " +
            $"database={Environment.GetEnvironmentVariable("FDDATABASE", EnvironmentVariableTarget.User)}; " +
            $"user={Environment.GetEnvironmentVariable("FDUSERNAME", EnvironmentVariableTarget.User)};" +
            $" password={Environment.GetEnvironmentVariable("FDPASSWORD", EnvironmentVariableTarget.User)};"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseAuthentication();

    //        app.UseStatusCodePages(
    //"text/plain", "Status code page, status code: {0}");
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

            services.AddScoped<IBadgeService, MockBadgeService>();
            services.AddScoped<IPitchService, MockPitchService>();
        }
    }
}