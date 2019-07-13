using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Extensions;

namespace ferrilata_devilline.IntegrationTests.Fixtures
{
    public class TestContext : IDisposable
    {
        private TestServer server;
        public HttpClient Client { get; set; }
        public ApplicationContext Context { get; set; }

        public TestContext()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            server = new TestServer(builder);
            Client = server.CreateClient();

            Context = (ApplicationContext)server.Host.Services.GetService(typeof(ApplicationContext));
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