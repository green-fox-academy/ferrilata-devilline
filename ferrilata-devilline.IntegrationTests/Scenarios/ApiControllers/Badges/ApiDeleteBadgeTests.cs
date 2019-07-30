using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Common;
using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Services.Interfaces;
using Newtonsoft.Json;
using Xunit;

namespace ferrilata_devilline.IntegrationTests.Scenarios.ApiControllers.Badges
{
    [Collection("BaseCollection")]
    public class ApiDeleteBadgeTests
    {
        private readonly TestContext testContext;
        private readonly ITokenService _tokenService;
        private readonly string email;


        public ApiDeleteBadgeTests()
        {
            testContext = new TestContext();
            _tokenService = testContext.TokenService;
            email = "useremail@ferillata.com";
        }

        [Fact]
        public async Task DeleteBadgeApi_IncorrectAuthentication_ShouldMessageEqual()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/badges/2");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Unauthorized",
                JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString)["error"]);
        }

        [Fact]
        public async Task DeleteBadgeApi_CorrectAuthentication_ShouldReturnOK()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/badges/1");
            request.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            var response = await testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteBadgeApi_CorrectAuthentication_ShouldDeleteBadge()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/badges/1");
            request.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            await testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            Assert.True(testContext.Context.Badges.Count(badge => badge.BadgeId == 1) == 0);
        }

        [Fact]
        public async Task DeleteBadgeApi_CorrectAuthentication_ShouldDeleteLevelsAssociatedWithBadge()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/badges/1");
            request.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            var levels = testContext.Context.Badges.GetItemByIndex(0).Levels;

            await testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            for (var i = 0;
                i < levels.Count;
                i++)
            {
                Assert.Empty(testContext.Context.Levels.Where(l => l.LevelId == levels[i].LevelId));
            }
        }

        [Fact]
        public async Task DeleteBadgeApi_CorrectAuthentication_ShouldDeletePitchAssociatedWithBadge()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/badges/1");
            request.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            var levels = testContext.Context.Badges.GetItemByIndex(0).Levels;
            var pitches = levels.SelectMany(l => l.Pitches).ToList();

            await testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            for (var i = 0; i < pitches.Count; i++)
            {
                Assert.Empty(testContext.Context.Pitches.Where(p => p.PitchId == pitches[i].PitchId));
            }
        }

        [Fact]
        public async Task DeleteBadgeApi_CorrectAuthentication_ShouldDeleteUserLevelAssociatedWithBadge()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/badges/1");
            request.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            var levels = testContext.Context.Badges.GetItemByIndex(0).Levels;
            var userLevels = levels.SelectMany(l => l.UserLevels).ToList();

            await testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            for (var i = 0; i < userLevels.Count; i++)
            {
                Assert.Empty(testContext.Context.UserLevels.Where(ul => ul.LevelId == userLevels[i].LevelId));
            }
        }

        [Fact]
        public async Task DeleteBadgeApi_CorrectAuthentication_ShouldDeleteReviewAssociatedWithBadge()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/badges/1");
            request.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            var levels = testContext.Context.Badges.GetItemByIndex(0).Levels;
            var pitches = levels.SelectMany(l => l.Pitches).ToList();
            var reviews = pitches.SelectMany(p => p.Reviews).ToList();

            await testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            for (var i = 0; i < reviews.Count; i++)
            {
                Assert.Empty(testContext.Context.Reviews.Where(r => r.ReviewId == reviews[i].ReviewId));
            }
        }

        [Fact]
        public async Task DeleteBadgeApi_CorrectAuthentication_ShouldDeletePitchAssociatedWithBadgeFromUser()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/badges/1");
            request.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            var levels = testContext.Context.Badges.GetItemByIndex(0).Levels;
            var pitches = levels.SelectMany(l => l.Pitches).ToList();

            await testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            foreach (var t in pitches)
            {
                Assert.Empty(testContext.Context.Users.SelectMany(u => u.Pitches).ToList()
                    .FindAll(p => p.PitchId == t.PitchId));
            }
        }

        [Fact]
        public async Task DeleteBadgeApi_CorrectAuthentication_ShouldDeleteReviewAssociatedWithBadgeFromUser()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/badges/1");
            request.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            var levels = testContext.Context.Badges.GetItemByIndex(0).Levels;
            var pitches = levels.SelectMany(l => l.Pitches).ToList();
            var reviews = pitches.SelectMany(p => p.Reviews).ToList();

            await testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            foreach (var t in reviews)
            {
                Assert.Empty(testContext.Context.Users.SelectMany(u => u.Reviews).ToList()
                    .FindAll(r => r.ReviewId == t.ReviewId));
            }
        }
    }
}