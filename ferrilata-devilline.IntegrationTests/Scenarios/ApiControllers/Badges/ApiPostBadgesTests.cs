using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;

namespace ferrilata_devilline.IntegrationTests.Scenarios.ApiControllers.Badges
{
    [Collection("BaseCollection")]
    public class ApiPostBadgesTests
    {
        private readonly TestContext testContext;
        private readonly ITokenService _tokenService;
        private readonly string email;

        public ApiPostBadgesTests()
        {
            testContext = new TestContext();
            _tokenService = this.testContext.TokenService;
            email = "useremail@ferillata.com";
        }

        [Theory]
        [InlineData("api/post/badges")]
        public async Task PostBadgesApi_CorrectAuthentication_ShouldReturnCreated(string url)
        {
            var newBadge = GenerateBadge();
            string PostingJson = JsonConvert.SerializeObject(newBadge);

            var client = testContext.Client;
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            var response = await client.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData("api/post/badges")]
        public async Task PostBadgesApi_CorrectAuthentication_NewBadgeCreatedInDB(string url)
        {
            var newBadge = GenerateBadge();
            string PostingJson = JsonConvert.SerializeObject(newBadge);
            int badgesBeforePosting = testContext.Context.Badges.ToList().Count();

            var client = testContext.Client;
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            var response = await client.SendAsync(request);

            Assert.Equal(badgesBeforePosting + 1, testContext.Context.Badges.ToList().Count());
        }

        [Theory]
        [InlineData("api/post/badges")]
        public async Task PostBadgesApi_CorrectAuthentication_NewBadgeHasLevels(string url)
        {
            var newBadge = GenerateBadge();
            string PostingJson = JsonConvert.SerializeObject(newBadge);
            int numbeeOfNewLists = testContext
                .Context
                .Badges
                .Select(b => b.Levels).ToList().Count();

            int currentNumberOfLists = testContext.Context.Levels.ToList().Count();

            var client = testContext.Client;
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            var response = await client.SendAsync(request);

            Assert.Equal(numbeeOfNewLists + currentNumberOfLists, testContext.Context.Levels.ToList().Count());
        }

        public BadgeInDTO GenerateBadge()
        {
            List<LevelInDTO> fakelevels = new List<LevelInDTO>();
            fakelevels.Add(new LevelInDTO { LevelNumber = 1, Description = "ololo", Weight = "2" });
            fakelevels.Add(new LevelInDTO { LevelNumber = 2, Description = "ololobobob", Weight = "3" });

            BadgeInDTO NewBadge = new BadgeInDTO
            {
                Version = 1,
                Name = "Good jobber",
                Tag = "1",
                Levels = fakelevels
            };

            return NewBadge;
        }
    }
}
