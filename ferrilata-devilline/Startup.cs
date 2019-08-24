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
using System.Data.SqlClient;
using System.Net;
using System.Text;
using ferrilata_devilline.Services.Helpers.AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Swagger;

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
                }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("FDTOKENSECRET"))),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };

                    x.Events = new JwtBearerEvents();
                    x.Events.OnChallenge = context =>
                    {
                        // Skip the default logic.
                        context.HandleResponse();
                        context.Response.StatusCode = 401;

                        var payload = new JObject
                        {
                            ["error"] = "Unauthorized"
                        };

                        return context.Response.WriteAsync(payload.ToString());
                    };
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new Info { Title = "Level-Up", Version = "v1" })
            );
            services.AddDbContext<ApplicationContext>(builder => builder
                .UseMySQL($"server={Environment.GetEnvironmentVariable("FDHOST")}; " +
                          $"database={Environment.GetEnvironmentVariable("FDDATABASE")}; " +
                          $"user={Environment.GetEnvironmentVariable("FDUSERNAME")};" +
                          $" password={Environment.GetEnvironmentVariable("FDPASSWORD")};"));

            services.Configure<SlackOptions>(Configuration.GetSection("SlackOptions"));

            services.SetUpAutoMapper();

            services.AddScoped<IBadgeRepository, BadgeRepository>();
            services.AddScoped<IBadgeService, BadgeService>();
            services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILevelService, LevelService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPitchRepository, PitchRepository>();
            services.AddScoped<IPitchService, PitchService>();
            services.AddScoped<ISlackMessagingService, SlackMessagingService>();
            services.AddScoped<ITokenService, TokenService>();

            services.BuildServiceProvider()
                .GetRequiredService<ApplicationContext>().SeedWithData();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Level-Up v1")
            );
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
 
                    var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    if (contextFeature?.Error is MySqlException)
                    {
                        await context.Response.WriteAsync(new
                        {
                            context.Response.StatusCode,
                            Message = "Database is down"
                        }.ToString());
                    }
                });
            });

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
            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new Info { Title = "Level-Up", Version = "v1" })
            );
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
            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new Info { Title = "Level-Up", Version = "v1" })
            );
            services.SetUpAutoMapper();

            services.AddDbContext<ApplicationContext>(builder => builder.UseInMemoryDatabase("InMemory"),
                ServiceLifetime.Singleton);
            services.AddScoped<IBadgeRepository, BadgeRepository>();
            services.AddScoped<IBadgeService, BadgeService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddScoped<ILevelService, LevelService>();

            services.AddAuthentication(options =>
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection =
                        Configuration.GetSection("Authentication:Google");

                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("secretTestingKey")),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };

                    x.Events = new JwtBearerEvents();
                    x.Events.OnChallenge = context =>
                    {
                        // Skip the default logic.
                        context.HandleResponse();
                        context.Response.StatusCode = 401;

                        var payload = new JObject
                        {
                            ["error"] = "Unauthorized"
                        };

                        return context.Response.WriteAsync(payload.ToString());
                    };
                });
        }
    }
}