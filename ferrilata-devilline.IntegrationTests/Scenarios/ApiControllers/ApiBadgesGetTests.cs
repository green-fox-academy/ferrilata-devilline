using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DAOs;
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
    public class ApiBadgesGetTest
    {
        private readonly TestContext _testContext;
        private readonly HttpRequestMessage _request;

        public ApiBadgesGetTest(TestContext testContext)
        {
            _testContext = testContext;
            _request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
        }

        [Fact]
        public async Task GetBadgesApi_CorrectAuthentication_ShouldReturnOK()
        {
            _request.Headers.Add("Authorization", "test");
            var response = await _testContext.Client.SendAsync(_request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetBadgesApi_AuthorizationHeader_IsMissing_ShouldReturnUnathorized()
        {
            var response = await _testContext.Client.SendAsync(_request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetBadgesApi_CorrectAuthentication_ShouldReturn_BodyTypeBadgeBase()
        {
            _request.Headers.Add("Authorization", "test");
            var response = await _testContext.Client.SendAsync(_request);
            var responseString = await response.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject<List<Badge>>(responseString);
        }

        [Fact]
        public async Task GetBadgesApi_CorrectAuthentication_ShouldReturn_CorrectBadges()
        {
            _request.Headers.Add("Authorization", "test");
            var response = await _testContext.Client.SendAsync(_request);
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<Badge>>(responseString);
            Assert.Equal("another badge", actual[1].Name);
            Assert.Equal("another level description", actual[1].Levels[0].Description);
            Assert.Equal("balazs.barna", actual[1].Levels[0].Holders[0].Name);
        }

        [Fact]
        public async Task GetBadgesApi_IncorrectAuthentication_ShouldMessageequal()
        {
            var response = await _testContext.Client.SendAsync(_request);
            var responseString = await response.Content.ReadAsStringAsync();   
            Assert.Equal("Unauthorized", JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString)["error"]);
        }
    }
}
