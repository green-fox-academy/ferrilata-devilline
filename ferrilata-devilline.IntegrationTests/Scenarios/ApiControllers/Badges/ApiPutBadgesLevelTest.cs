using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models.DTOs;
using Newtonsoft.Json;
using Xunit;

namespace ferrilata_devilline.IntegrationTests.Scenarios.ApiControllers.Badges
{
    [Collection("BaseCollection")]
    public class ApiPutBadgesLevelTest
    {
        private readonly TestContext _testContext;
        private string token;

        public ApiPutBadgesLevelTest(TestContext testContext)
        {
            _testContext = testContext;
            token = "Bearer " + _testContext.TokenService.GenerateToken("useremail@ferillata.com", true);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public async Task PutLevelById_CorrectAuthentication_CorrectLevelByBadge_ShouldReturnStatusOk(
            long badgeId, long levelId)
        {
            string input = JsonConvert.SerializeObject(createTestLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/badges/{badgeId}/levels/{levelId}");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(input,
                Encoding.UTF8,
                "application/json");
            var response = await _testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public async Task
            PutLevelById_CorrectAuthentication_CorrectLevelByBadge_ShouldReturnMessage_Updated(long badgeId,
                long levelId)
        {
            string input = JsonConvert.SerializeObject(createTestLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/badges/{badgeId}/levels/{levelId}");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(input,
                Encoding.UTF8,
                "application/json");
            var response = await _testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(new {message = "Updated"}), responseString);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public async Task PutLevelById_CorrectAuthentication_CorrectLevelByBadge_ShouldUpdateLevel(
            long badgeId, long levelId)
        {
            var input = createTestLevelInDTO();
            string inputString = JsonConvert.SerializeObject(input);
            var request = new HttpRequestMessage(HttpMethod.Post, $"/api/badges/{badgeId}/levels/{levelId}");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(inputString,
                Encoding.UTF8,
                "application/json");
            var expected = _testContext.Context.Levels.Find(levelId);
            expected.Weight = input.Weight;
            expected.Description = input.Description;
            expected.LevelNumber = input.LevelNumber;
            var response = await _testContext.Client.SendAsync(request);
            var actual = _testContext.Context.Badges.SelectMany(b => b.Levels)
                .FirstOrDefault(l => l.LevelId == levelId);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        public async Task PutLevelById_CorrectAuthentication_IncorrectLevelByBadge_ShouldReturnErrorMessage(long badgeId, long levelId)
        {
            string inputString = JsonConvert.SerializeObject(createTestLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/badges/{badgeId}/levels/{levelId}");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(inputString,
                Encoding.UTF8,
                "application/json");
            var response = await _testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(new {error = "No such level found for the selected badge"}), responseString);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        public async Task PutLevelById_CorrectAuthentication_IncorrectLevelByBadge_ShouldReturnNotFound(long badgeId, long levelId)
        {
            string input = JsonConvert.SerializeObject(createTestLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Put, $"/api/badges/{badgeId}/levels/{levelId}");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(input,
                Encoding.UTF8,
                "application/json");
            var response = await _testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PutLevelById_InCorrectAuthentication_ShouldReturnUnauthorized()
        {
            string input = JsonConvert.SerializeObject(createTestLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Put, "/api/badges/1/levels/1");
            request.Headers.Add("Authorization", "test");
            request.Content = new StringContent(input,
                Encoding.UTF8,
                "application/json");
            var response = await _testContext.Client.SendAsync(request);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        private LevelInDTO createTestLevelInDTO()
        {
            return new LevelInDTO {Weight = "test", LevelNumber = 2, Description = "test"};
        }
    }
}