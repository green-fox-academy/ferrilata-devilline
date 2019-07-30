using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        private readonly TestContext testContext;
        private readonly ITokenService _tokenService;
        private readonly ILevelService _levelService;
        
        private string token;

        public PostLevelByBadgeIdTests(TestContext testContext)
        {
            this.testContext = testContext;
            _tokenService = this.testContext.TokenService;
            token = "Bearer " + _tokenService.GenerateToken("useremail@ferillata.com", true);
            _levelService = this.testContext.LevelService;
        }

        [Fact]
        public async Task PostLevelById_CorrectAuthentication_CorrectBody_ShouldReturnCreated()
        {
            string PostingJson = JsonConvert.SerializeObject(createCorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task PostLevelById_CorrectAuthentication_CorrectBody_ShouldReturnMessage_Created()
        {
            string PostingJson = JsonConvert.SerializeObject(createCorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(new {message = "Created"}), responseString);
        }

        [Fact]
        public async Task PostLevelById_CorrectAuthentication_CorrectBody_ShouldCreateNewLevel()
        {
            string PostingJson = JsonConvert.SerializeObject(createCorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await testContext.Client.SendAsync(request);
            bool expected = _levelService.FindById(3) != null;

            Assert.True(expected);
        }

        [Fact]
        public async Task PostLevelById_InCorrectAuthentication_ShouldReturnUnauthorized()
        {
            string PostingJson = JsonConvert.SerializeObject(createCorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token.Substring(0, 20));
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task PostLevelById_InCorrectAuthentication_ShouldReturnMessage_Unauthorized()
        {
            string PostingJson = JsonConvert.SerializeObject(createCorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token.Substring(0, 20));
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(new { error = "Unauthorized" }),
                "{" + responseString.Substring(4, 23).Replace(" ", "") + "\"}");
        }

        [Fact]
        public async Task PostLevelById_InCorrectBody_ShouldReturnNotFound()
        {
            string PostingJson = JsonConvert.SerializeObject(createIncorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PostLevelById_InCorrectBody_ShouldReturnMessage()
        {
            string PostingJson = JsonConvert.SerializeObject(createIncorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(new { error = "Please provide all fields"}), responseString);
        }

        [Fact]
        public async Task PostLevelById_IncorrectUrl_ShouldReturnNotFound()
        {
            string PostingJson = JsonConvert.SerializeObject(createIncorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/3/levels");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PostLevelById_IncorrectUrl_ShouldReturnMessage()
        {
            string PostingJson = JsonConvert.SerializeObject(createIncorrectLevelInDTO());
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/badges/3/levels");
            request.Headers.Add("Authorization", token);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(new {error = "Please provide an existing Badge Id"}), responseString);
        }

        public LevelInDTO createCorrectLevelInDTO()
        {
            return new LevelInDTO() {Weight = "test", LevelNumber = 2, Description = "test"};
        }

        public Object createIncorrectLevelInDTO()
        {
            return new {LevelNumber = 2, Description = "test"};
        }
    }
}
