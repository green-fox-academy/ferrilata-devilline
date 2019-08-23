using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ferrilata_devilline.IntegrationTests.Scenarios.ApiControllers.Badges
{
    [Collection("BaseCollection")]
    public class PostLevelByBadgeIdTests
    {
        private readonly TestContext _testContext;
        private readonly ITokenService _tokenService;
        private string token;

        public PostLevelByBadgeIdTests()
        {
            _testContext = new TestContext();
            _tokenService = _testContext.TokenService;
            token = "Bearer " + _tokenService.GenerateToken("useremail@ferillata.com", true);
        }

        [Fact]
        public async Task PostLevelById_CorrectAuthentication_CorrectBody_ShouldReturnCreated()
        {
            string postingJson = JsonConvert.SerializeObject(createCorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(postingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await _testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task PostLevelById_CorrectAuthentication_CorrectBody_ShouldReturnMessage_Created()
        {
            string postingJson = JsonConvert.SerializeObject(createCorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(postingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await _testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(new { message = "Created" }), responseString);
        }

        [Fact]
        public async Task PostLevelById_CorrectAuthentication_ExistingLevelBody_ShouldReturnBadRequest()
        {
            string postingJson = JsonConvert.SerializeObject(createExistingInCorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(postingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await _testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostLevelById_CorrectAuthentication_ExistingLevelBody_ShouldReturnMessage_BadRequest()
        {
            string postingJson = JsonConvert.SerializeObject(createExistingInCorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(postingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await _testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(new { error = "This badge already has a level of this number" }), responseString);
        }

        [Fact]
        public async Task PostLevelById_CorrectAuthentication_CorrectBody_ShouldCreateNewLevel()
        {
            string postingJson = JsonConvert.SerializeObject(createCorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(postingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await _testContext.Client.SendAsync(request);

            Assert.True(_testContext.Context.Badges.Where(l => l.BadgeId == 3) != null);
        }

        [Fact]
        public async Task PostLevelById_CorrectAuthentication_CorrectBody_ShouldAddNewLevel_ToBadgeWithId_1()
        {
            string postingJson = JsonConvert.SerializeObject(createCorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(postingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await _testContext.Client.SendAsync(request);

            Assert.True(_testContext.Context.Badges.First(b => b.BadgeId == 1).Levels.Where(l => l.LevelId == 3) != null);
        }

        [Fact]
        public async Task PostLevelById_InCorrectAuthentication_ShouldReturnUnauthorized()
        {
            string postingJson = JsonConvert.SerializeObject(createCorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token.Substring(0, 20));
            request.Content = new StringContent(postingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await _testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task PostLevelById_InCorrectAuthentication_ShouldReturnMessage_Unauthorized()
        {
            string postingJson = JsonConvert.SerializeObject(createCorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token.Substring(0, 20));
            request.Content = new StringContent(postingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await _testContext.Client.SendAsync(request);
            string responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("Unauthorized",
                JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString)["error"]);
        }

        [Fact]
        public async Task PostLevelById_InCorrectBody_ShouldReturnBadRequest()
        {
            string postingJson = JsonConvert.SerializeObject(createIncorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(postingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await _testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostLevelById_InCorrectBody_ShouldReturnMessage()
        {
            string postingJson = JsonConvert.SerializeObject(createIncorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(postingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await _testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(new { error = "Please provide all fields" }), responseString);
        }

        [Fact]
        public async Task PostLevelById_IncorrectUrl_ShouldReturnBadRequest()
        {
            string postingJson = JsonConvert.SerializeObject(createIncorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/3/levels");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(postingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await _testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostLevelById_IncorrectUrl_ShouldReturnMessage()
        {
            string postingJson = JsonConvert.SerializeObject(createIncorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/10/levels");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(postingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await _testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(new { error = "Please provide an existing Badge Id" }), responseString);
        }

        public LevelInDTO createCorrectLevelInDTO()
        {
            return new LevelInDTO() { Weight = "test", LevelNumber = 100, Description = "test" };
        }

        public LevelInDTO createExistingInCorrectLevelInDTO()
        {
            return new LevelInDTO() { Weight = "test", LevelNumber = 1, Description = "test" };
        }

        public Object createIncorrectLevelInDTO()
        {
            return new { LevelNumber = 2, Description = "test" };
        }
    }
}
