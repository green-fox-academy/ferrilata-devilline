using ferrilata_devilline.IntegrationTests.Fixtures;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Common;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using Xunit;

namespace ferrilata_devilline.IntegrationTests
{
    [Collection("BaseCollection")]
    public class ApiBadgesTest
    {
        private readonly TestContext testContext;

        public ApiBadgesTest(TestContext testContext)
        {
            this.testContext = testContext;
        }

        [Fact]
        public async Task GetBadgesApi_CorrectAuthentication_ShouldReturnOK()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            request.Headers.Add("Authorization", "test");
            var response = testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetBadgesApi_AuthorizationHeader_IsMissing_ShouldReturnUnauthorized()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            var response = await testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetBadgesApi_CorrectAuthentication_ShouldReturn_BodyTypeBadgeBase()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            request.Headers.Add("Authorization", "test");
            var response = testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            var actual = JsonConvert.DeserializeObject<List<BadgeDTO>>(responseString);
            Assert.True(actual.GetType() == typeof(List<BadgeDTO>));
        }

        [Fact]
        public async Task GetBadgesApi_IncorrectAuthentication_ShouldMessageEqual()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            var response = await testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Unauthorized",
                JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString)["error"]);
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
            request.Headers.Add("Authorization", "test");
            var response = await testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteBadgeApi_CorrectAuthentication_ShouldDeleteBadge()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/badges/1");
            request.Headers.Add("Authorization", "test");
            await testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            Assert.True(testContext.Context.Badges.Count(badge => badge.BadgeId == 1) == 0);
        }

        [Fact]
        public async Task DeleteBadgeApi_CorrectAuthentication_ShouldDeleteLevelsAssociatedWithBadge()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/badges/1");
            request.Headers.Add("Authorization", "test");
            var levels = testContext.Context.Badges.GetItemByIndex(0).Levels;
            await testContext.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            for (var i = 0; i < levels.Count; i++)
            {
                Assert.Empty(testContext.Context.Levels.Where(l => l.LevelId == levels[i].LevelId));
            }
        }

        [Fact]
        public async Task DeleteBadgeApi_CorrectAuthentication_ShouldDeletePitchAssociatedWithBadge()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/badges/1");
            request.Headers.Add("Authorization", "test");
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
            request.Headers.Add("Authorization", "test");
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
            request.Headers.Add("Authorization", "test");
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
            request.Headers.Add("Authorization", "test");
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
            request.Headers.Add("Authorization", "test");
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