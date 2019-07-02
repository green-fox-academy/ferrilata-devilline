using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;


namespace ferrilata_devilline.IntegrationTests.Fixtures
{
    public class TestContext : IDisposable
    {
        private TestServer server;
        public HttpClient Client { get; set; }


        public TestContext()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            server = new TestServer(builder);
            Client = server.CreateClient();
        }

        public void Dispose()
        {
            server.Dispose();
            Client.Dispose();
        }
    }
}