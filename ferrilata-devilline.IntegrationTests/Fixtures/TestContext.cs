using System;
using System.Net.Http;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ferrilata_devilline.IntegrationTests.Fixtures
{
    public class TestContext : IDisposable
    {
        private TestServer server;
        public HttpClient Client { get; set; }
        public ITokenService TokenService { get; set; }


        public TestContext()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("/Users/yarik/Desktop/ferrilata-develline/ferrilata-devilline/" +
                "ferrilata-devilline/appsettings.Testing.json", optional: false, reloadOnChange: false)
             .AddEnvironmentVariables()
             .Build();

            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseConfiguration(config)
                .UseStartup<Startup>();

            server = new TestServer(builder);
            Client = server.CreateClient();
            TokenService = server.Host.Services.GetRequiredService<ITokenService>();
        }

        public void Dispose()
        {
            server.Dispose();
            Client.Dispose();
        }
    }
}