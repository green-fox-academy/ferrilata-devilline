using ferrilata_devilline.IntegrationTests.Fixtures;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ferrilata_devilline.Models.DTOs;
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
            var response = testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetBadgesApi_AuthorizationHeader_IsMissing_ShouldReturnUnauthorized()
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
            var response = testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            var actual = JsonConvert.DeserializeObject<List<BadgeDTO>>(responseString);
            Assert.True(actual.GetType() == typeof(List<BadgeDTO>));
        }

        [Fact]
        public async Task GetBadgesApi_IncorrectAuthentication_ShouldMessageEqual()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Unauthorized",
                JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString)["error"]);
        }

        [Fact]
        public async Task DeleteBadgeApi_IncorrectAuthentication_ShouldMessageEqual()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/badges/2");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Unauthorized",
                JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString)["error"]);
        }

        [Fact]
        public async Task DeleteBadgeApi_CorrectAuthentication_ShouldReturnOK()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/badges/1");
            request.Headers.Add("Authorization", "test");
            var response = await testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteBadgeApi_CorrectAuthentication_ShouldDeleteBadge()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/badges/1");
            request.Headers.Add("Authorization", "test");
            await testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            Assert.True(testContext.Context.Badges.Count(badge => badge.BadgeId == 1) == 0);
        }
        
    }
}