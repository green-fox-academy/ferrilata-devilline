using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models.DTOs.Out;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using ferrilata_devilline.Models.DTOs;

namespace ferrilata_devilline.IntegrationTests
{
    [Collection("BaseCollection")]
    public class ApiBadgesTest
    {
        private readonly TestContext testContext;
        private ApplicationContext _context;

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
        public async Task GetBadgesApi_CorrectAuthentication_ShouldReturn_BodyTypeBadgeOutDTO()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            request.Headers.Add("Authorization", "test");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<BadgeOutDTO>>(responseString);
            Assert.True(actual.GetType() == typeof(List<BadgeOutDTO>));
        }

        [Fact]
        public async Task GetBadgesApi_CorrectAuthentication_ShouldReturn_CorrectBadges()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            request.Headers.Add("Authorization", "test");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<BadgeOutDTO>>(responseString);

            Assert.Equal("badge1 name", actual[0].Name);
            Assert.Equal("user1 name", actual[0].Levels[0].Holders[0].Name);

            Assert.Equal("badge2 tag", actual[1].Tag);
            Assert.Equal("level2 description", actual[1].Levels[0].Description);
            Assert.Equal("2", actual[1].Levels[1].Holders[0].PersonId);
        }

        [Fact]
        public async Task GetBadgesApi_IncorrectAuthentication_ShouldMessageequal()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Unauthorized",
                JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString)["error"]);
        }

        [Fact]
        public async Task temporaryTest()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            request.Headers.Add("Authorization", "test");

            var response = await testContext.Client.SendAsync(request);

            Assert.Equal("Microsoft.EntityFrameworkCore.InMemory", testContext.Context.Database.ProviderName);
        }
    }
}