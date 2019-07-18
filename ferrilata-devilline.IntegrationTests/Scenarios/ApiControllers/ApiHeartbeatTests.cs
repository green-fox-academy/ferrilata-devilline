using ferrilata_devilline.IntegrationTests.Fixtures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ferrilata_devilline.IntegrationTests.Scenarios.ApiControllers
{
    [Collection("BaseCollection")]
    public class ApiHeartbeatTests
    {
        private readonly TestContext testContext;

        public ApiHeartbeatTests(TestContext testContext)
        {
            this.testContext = testContext;
        }

        [Fact]
        public async Task GetHeartbeat_CorrectAuthentication_ShouldReturnOK()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/heartbeat");
            request.Headers.Add("Authorization", "test");
            var response = await testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetHeartbeat_IncorrectAuthentication_ShouldReturnUnathorized()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/heartbeat");
            var response = await testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetHeartbeat_CorrectAuthentication_ShouldMessageequal_OK()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/heartbeat");
            request.Headers.Add("Authorization", "test");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(JsonConvert.SerializeObject(new { status = "OK" }), responseString);
        }

        [Fact]
        public async Task GetHeartbeat_IncorrectAuthentication_ShouldMessageequal_Unauthorized()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/heartbeat");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(JsonConvert.SerializeObject(new { error = "Unauthorized" }), responseString);
        }
    }
}
