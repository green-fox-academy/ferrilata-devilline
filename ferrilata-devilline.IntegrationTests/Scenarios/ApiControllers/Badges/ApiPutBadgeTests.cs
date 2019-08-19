using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using Newtonsoft.Json;
using Xunit;

namespace ferrilata_devilline.IntegrationTests.Scenarios.ApiControllers.Badges
{
    [Collection("BaseCollection")]
    public class ApiPutBadgeTests
    {
        private readonly TestContext _testContext;
        private string token;
        private readonly BadgeInDTO badgeToUpdate;

        public ApiPutBadgeTests(TestContext testContext)
        {
            _testContext = testContext;
            token = "Bearer " + _testContext.TokenService.GenerateToken("useremail@ferillata.com", true);
            badgeToUpdate = ApiPostBadgesTests.GenerateBadge();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task PutBadgeById_CorrectAuthentication_ShouldReturnStatusOk(
            long badgeId)
        {
            string input = JsonConvert.SerializeObject(badgeToUpdate);
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/badges/{badgeId}");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(input,
                Encoding.UTF8,
                "application/json");
            var response = await _testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task
            PutBadgeById_CorrectAuthentication_ShouldReturnMessage_Updated(long badgeId)
        {
            string input = JsonConvert.SerializeObject(badgeToUpdate);
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/badges/{badgeId}");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(input,
                Encoding.UTF8,
                "application/json");
            var response = await _testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(new {message = "Updated"}), responseString);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task PutBadgeById_CorrectAuthentication_ShouldUpdateBadge(
            long badgeId)
        {
            string inputString = JsonConvert.SerializeObject(badgeToUpdate);
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/badges/{badgeId}");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(inputString,
                Encoding.UTF8,
                "application/json");
            var expected = new Badge
            {
                BadgeId = badgeId,
                Tag = badgeToUpdate.Tag,
                Version = badgeToUpdate.Version,
                Name = badgeToUpdate.Name,
                Levels = new List<Level>()
            };
            var highestLevelId = _testContext.Context.Levels.Max(l => l.LevelId);
            foreach (var level in badgeToUpdate.Levels.ToList())
            {
                var newLevel = new Level
                {
                    LevelNumber = level.LevelNumber,
                    Description = level.Description,
                    Weight = level.Weight,
                    LevelId = highestLevelId + 1,
                    Badge = expected
                };
                highestLevelId++;
                expected.Levels.Add(newLevel);
            }

            var response = await _testContext.Client.SendAsync(request);
            var actual = _testContext.Context.Badges.Find(badgeId);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public async Task PutBadgeById_CorrectAuthentication_IncorrectBadgeId_ShouldReturnErrorMessage(
            long badgeId)
        {
            string inputString = JsonConvert.SerializeObject(badgeToUpdate);
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/badges/{badgeId}");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(inputString,
                Encoding.UTF8,
                "application/json");
            var response = await _testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(new {error = "No badge with the provided id exists"}),
                responseString);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public async Task PutBadgeById_CorrectAuthentication_IncorrectBadgeId_ShouldReturnNotFound(long badgeId)
        {
            string input = JsonConvert.SerializeObject(badgeToUpdate);
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/badges/{badgeId}");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(input,
                Encoding.UTF8,
                "application/json");
            var response = await _testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PutBadgeById_InCorrectAuthentication_ShouldReturnUnauthorized()
        {
            string input = JsonConvert.SerializeObject(badgeToUpdate);
            var request = new HttpRequestMessage(HttpMethod.Put, "/api/badges/1");
            request.Headers.Add("Authorization", "test");
            request.Content = new StringContent(input,
                Encoding.UTF8,
                "application/json");
            var response = await _testContext.Client.SendAsync(request);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}