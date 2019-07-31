using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Interfaces;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ferrilata_devilline.IntegrationTests.Scenarios.ApiControllers.Badges
{
    [Collection("BaseCollection")]
    public class GetLevelByIds
    {
        private readonly TestContext _testContext;
        private readonly ITokenService _tokenService;

        private string token;

        public GetLevelByIds()
        {
            _testContext = new TestContext();
            _tokenService = _testContext.TokenService;
            token = "Bearer " + _tokenService.GenerateToken("useremail@ferillata.com", true);
        }

        [Fact]
        public async Task GetLevelByIds_CorrectAuthentication_ShouldReturnOK()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges/1/levels/1");
            request.Headers.Add("Authorization", token);
            var response = await _testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetLevelByIds_CorrectAuthentication_ShouldReturn_BodyTypeLevelOutDTO()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges/1/levels/1");
            request.Headers.Add("Authorization", token);
            var response = await _testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<LevelOutDTO>(responseString);
            Assert.True(actual.GetType() == typeof(LevelOutDTO));
        }

        [Fact]
        public async Task GetLevelByIds_InCorrectAuthentication_ShouldReturnUnauthorized()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges/1/levels/1");
            var response = await _testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetBadgesByIdApi_Unexistingid_CorrectAuthentication_ShouldReturnMessage_BadRequest()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges/2/levels/1");
            request.Headers.Add("Authorization", token);
            var response = await _testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
