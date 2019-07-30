using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Xunit;

namespace ferrilata_devilline.IntegrationTests.Scenarios
{
    [Collection("BaseCollection")]
    public class ApiPostPitchTests
    {
        private readonly TestContext _testContext;
        private readonly ITokenService _tokenService;
        private readonly string email;

        public ApiPostPitchTests(TestContext testContext)
        {
            _testContext = testContext;
            _tokenService = _testContext.TokenService;
            email = "useremail@ferillata.com";
        }

        [Theory]
        [InlineData("api/post/pitch")]
        public async Task PostPitchCorrect_AuthorizationPresent(string url)
        {
            var newPosting = CreatePitchInDTO();
            string PostingJson = JsonConvert.SerializeObject(newPosting);

            var client = _testContext.Client;
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            var response = await client.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData("api/post/pitch")]
        public async Task PostPitchCorrect_AuthorizationMissing(string url)
        {
            var newPosting = CreatePitchInDTO();
            var PostingJson = JsonConvert.SerializeObject(newPosting);

            var client = _testContext.Client;
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await client.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData("api/post/pitch")]
        public async Task PostPitchMissingPropertytAuthorizationOK(string url)
        {
            var newPosting = new Pitch { };
            string PostingJson = JsonConvert.SerializeObject(newPosting);

            var client = _testContext.Client;
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            var response = await client.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("api/post/pitch")]
        public async Task PostPitchOKPropertytAuthorizationOK_TestMessage(string url)
        {
            var newPosting = CreatePitchInDTO();
            string PostingJson = JsonConvert.SerializeObject(newPosting);

            var client = _testContext.Client;
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            var response = await client.SendAsync(request);
            string ResponseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(new { message = "Created" }), ResponseBody);
        }

        [Theory]
        [InlineData("api/post/pitch")]
        public async Task PostPitchOKPropertytAuthorizationOK_TestUnauthorized(string url)
        {
            var newPosting = CreatePitchInDTO();
            string PostingJson = JsonConvert.SerializeObject(newPosting);

            var client = _testContext.Client;
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await client.SendAsync(request);
            string responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("Unauthorized",
                JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString)["error"]);
        }

        [Theory]
        [InlineData("api/post/pitch")]
        public async Task PostPitchOKPropertytAuthorizationOK_TestMissingField(string url)
        {
            var newPosting = new Pitch { };
            string PostingJson = JsonConvert.SerializeObject(newPosting);

            var client = _testContext.Client;
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            var response = await client.SendAsync(request);
            string ResponseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(new { error = "Please provide all fields" }),
              ResponseBody);
        }

        public PitchInDTO CreatePitchInDTO()
        {
            List<ReviewDTO> fakeReviews = new List<ReviewDTO>();
            PitchInDTO NewPitch = new PitchInDTO
            {
                Status = "status",
                PitchedMessage = "Good jobber",
                PitchedLevel = "1",
                Result = "Good",
                Created = 1,
                User = new UserDTO(),
                Level = new LevelMiniDTO(),
                Reviews = fakeReviews
            };

            return NewPitch;
        }
    }
}
