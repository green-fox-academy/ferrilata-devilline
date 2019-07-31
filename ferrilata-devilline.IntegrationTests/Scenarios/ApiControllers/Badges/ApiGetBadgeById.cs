using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ferrilata_devilline.IntegrationTests.Scenarios.ApiControllers.Badges
{
    [Collection("BaseCollection")]
    public class ApiGetBadgeById
    {
        private readonly TestContext _testContext;
        private readonly ITokenService _tokenService;

        private string token;

        public ApiGetBadgeById(TestContext testContext)
        {
            _testContext = testContext;
            _tokenService = _testContext.TokenService;
            token = "Bearer " + _tokenService.GenerateToken("useremail@ferillata.com", true);
        }

        [Fact]
        public async Task GetBadgesByIdApi_CorrectAuthentication_ShouldReturnOK()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges/1");
            request.Headers.Add("Authorization", token);
            var response = await _testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetBadgesByIdApi_CorrectAuthentication_ShouldReturn_BodyTypeBadgeDTO()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges/1");
            request.Headers.Add("Authorization", token);
            var response = await _testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<BadgeDTO>(responseString);
            Assert.True(actual.GetType() == typeof(BadgeDTO));
        }

        [Fact]
        public async Task GetBadgesByIdApi_InCorrectAuthentication_ShouldReturnUnauthorized()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges/1");
            var response = await _testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetbadgesIdApi_InCorrectAuthentication_ShouldReturnUnauthorized()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges/1");
            var response = await _testContext.Client.SendAsync(request);
            string responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("Unauthorized",
                JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString)["error"]);
        }

        [Fact]
        public async Task GetBadgesByIdApi_Unexistingid_CorrectAuthentication_ShouldReturnNotFound()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges/-1");
            request.Headers.Add("Authorization", token);
            var response = await _testContext.Client.SendAsync(request);
            string responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal(JsonConvert.SerializeObject(new { error = "Please provide an existing Badge Id" }), responseString);
        }


        [Fact]
        public async Task GetBadgesByIdApi_Unexistingid_CorrectAuthentication_ShouldReturnMessage_NotFound()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges/-1");
            request.Headers.Add("Authorization", token);
            var response = await _testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
