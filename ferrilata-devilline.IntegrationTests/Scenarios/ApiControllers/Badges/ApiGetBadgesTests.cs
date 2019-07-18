using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models;
using ferrilata_devilline.Services.Interfaces;
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
        private readonly ITokenService _tokenService;
        private readonly string email;

        public ApiBadgesTest(TestContext testContext)
        {
            this.testContext = testContext;
            _tokenService = this.testContext.TokenService;
            email = "useremail@ferillata.com";
        }

        [Fact]
        public async Task GetBadgesApi_CorrectAuthentication_ShouldReturnOK()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            request.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
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
            request.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<Badge>>(responseString);
            Assert.True(actual.GetType() == typeof(List<Badge>));
        }

        [Fact]
        public async Task GetBadgesApi_CorrectAuthentication_ShouldReturn_CorrectBadges()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            request.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<Badge>>(responseString);
            Assert.Equal("another badge", actual[1].Name);
            Assert.Equal("another level description", actual[1].Levels[0].Description);
            Assert.Equal("balazs.barna", actual[1].Levels[0].Holders[0].Name);
        }

        [Fact]
        public async Task GetBadgesApi_IncorrectAuthentication_ShouldMessageequal()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(JsonConvert.SerializeObject(new { error = "Unauthorized" }),
               "{" + responseString.Substring(4, 23).Replace(" ", "") + "}");
        }
    }
}