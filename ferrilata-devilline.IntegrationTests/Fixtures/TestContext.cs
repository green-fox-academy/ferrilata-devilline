using System;
using System.Net.Http;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Helpers;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace ferrilata_devilline.IntegrationTests.Fixtures
{
    public class TestContext : IDisposable
    {
        private TestServer server;
        public HttpClient Client { get; set; }
        public ITokenService TokenService { get; set; }
        public IMapper testMapper;

        public ApplicationContext Context { get; set; }

        public TestContext()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            server = new TestServer(builder);
            Client = server.CreateClient();
            TokenService = server.Host.Services.GetRequiredService<ITokenService>();
            Context = server.Host.Services.GetRequiredService<ApplicationContext>();
            testMapper = server.Host.Services.GetRequiredService<IMapper>();

            Context.SeedWithData();
        }

        public void Dispose()
        {
            server.Dispose();
            Client.Dispose();
            Context.Dispose();
        }
    }
}