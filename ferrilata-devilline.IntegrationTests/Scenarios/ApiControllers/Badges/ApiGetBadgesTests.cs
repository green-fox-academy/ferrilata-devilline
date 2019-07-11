using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Repositories;
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
        public async Task GetBadgesApi_CorrectAuthentication_ShouldReturn_BodyTypeBadgeBase()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            request.Headers.Add("Authorization", "test");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<Badge>>(responseString);
            Assert.True(actual.GetType() == typeof(List<Badge>));
        }

        [Fact]
        public async Task GetBadgesApi_CorrectAuthentication_ShouldReturn_CorrectBadges()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            request.Headers.Add("Authorization", "test");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<Badge>>(responseString);
            Assert.Equal("badge1 name", actual[0].Name);
            Assert.Equal("badge2 tag", actual[1].Tag);
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
            var expected = new List<Badge>
            {
                new Badge
                    {
                        BadgeId = 1,
                        Name = "badge1 name",
                        Tag = "badge1 tag",
                        Version = 1
                },
                new Badge
                {
                        BadgeId = 2,
                        Name = "badge2 name",
                        Tag = "badge2 tag",
                        Version = 2
                }
            };

            var expectedString = JsonConvert.SerializeObject(expected);

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            request.Headers.Add("Authorization", "test");

            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("Microsoft.EntityFrameworkCore.InMemory", testContext.Context.Database.ProviderName);
            Assert.Equal(expectedString, responseString);
        }
    }
}