using System;
using System.Net.Http;
using ferrilata_devilline.Services;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;


namespace ferrilata_devilline.IntegrationTests.Fixtures
{
    public class TestContext : IDisposable
    {
        private TestServer Server;
        public HttpClient Client { get; set; }

        public TestContext()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            Server = new TestServer(builder);
            IBadgeService badgeService = Server.Host.Services.GetService(typeof(IBadgeService)) as MockBadgeService;
            Client = Server.CreateClient();
        }

        public void Dispose()
        {
            Server.Dispose();
            Client.Dispose();
        }
    }
}