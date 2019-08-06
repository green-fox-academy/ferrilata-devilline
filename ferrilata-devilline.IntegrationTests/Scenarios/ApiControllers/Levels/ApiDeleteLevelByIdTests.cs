using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Services.Interfaces;
using Newtonsoft.Json;
using Xunit;

namespace ferrilata_devilline.IntegrationTests.Scenarios.ApiControllers.Levels
{
    public class ApiDeleteLevelByIdTests
    {
        private readonly TestContext testContext;
        private readonly string token;

        public ApiDeleteLevelByIdTests()
        {
            testContext = new TestContext();
            token = "Bearer " + testContext.TokenService.GenerateToken("useremail@ferillata.com", true);
        }

        [Fact]
        public async Task DeleteLevelApi_IncorrectAuthentication_ShouldMessageEqual()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/badges/1/levels/1");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Unauthorized",
                JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString)["error"]);
        }

        [Fact]
        public async Task DeleteBadgeApi_CorrectAuthentication_ShouldDeleteLevel()
        {
            var requestedBadgeLevelCount = testContext
                .Context
                .Badges
                .Where(b => b.BadgeId == 1).FirstOrDefault().Levels.Count();

            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/badges/1/levels/1");
            request.Headers.Add("Authorization", token);
            await testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            Assert.True(testContext
                .Context
                .Badges
                .Where(b => b.BadgeId == 1).FirstOrDefault().Levels.Count() == requestedBadgeLevelCount - 1);
        }

        [Fact]
        public async Task DeleteBadgeApi_CorrectAuthentication_ShouldDeleteLevelTest2()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/badges/1/levels/1");
            request.Headers.Add("Authorization", token);
            await testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            Assert.True(testContext
                .Context
                .Levels
                .Where(b => b.LevelId == 1).FirstOrDefault() == null);
        }
    }
}
