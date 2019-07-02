using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ferrilata_devilline.IntegrationTests
{
    [Collection("BaseCollection")]
    public class ApiBadgesTest
    {
        private readonly TestContext testContext;

        public ApiBadgesTest(TestContext testContext)
        {
            this.testContext = testContext;
        }

        [Fact]
        public async Task GetBadgesApi_CorrectAuthentication_ShouldReturnOK()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            request.Headers.Add("Authorization", "test");
            var response = await testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetBadgesApi_AuthorizationHeader_IsMissing_ShouldReturnUnathorized()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            var response = await testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetBadgesApi_CorrectAuthentication_ShouldReturn_BodyTypeBadgeBase()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            request.Headers.Add("Authorization", "test");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<BadgesBase>(responseString);
            Assert.True(actual.GetType() == typeof(BadgesBase));
        }

        [Fact]
        public async Task GetBadgesApi_IncorrectAuthentication_ShouldMessageequal()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();   
            Assert.Equal("Unauthorized", JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString)["error"]);
        }
    }
}
