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
    public class ApiGetBadgeById
    {
        private readonly TestContext testContext;
        private readonly ITokenService _tokenService;
        private readonly string email;

        public ApiGetBadgeById(TestContext testContext)
        {
            this.testContext = testContext;
            _tokenService = this.testContext.TokenService;
            email = "useremail@ferillata.com";
        }

        [Fact]
        public async Task GetBadgesByIdApi_CorrectAuthentication_ShouldReturnOK()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges/1");
            request.Headers.Add("Authorization", "test");
            var response = await testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetBadgesByIdApi_InCorrectAuthentication_ShouldReturnOK()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges/1");
            var response = await testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetBadgesByIdApi_Unexistingid_CorrectAuthentication_ShouldReturnOK()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges/-1");
            request.Headers.Add("Authorization", "test");
            var response = await testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetBadgesByIdApi_CorrectAuthentication_ShouldReturn_BodyTypeBadgeDTO()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges/1");
            request.Headers.Add("Authorization", "test");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<BadgeDTO>(responseString);
            Assert.True(actual.GetType() == typeof(BadgeDTO));
        }
    }
}
