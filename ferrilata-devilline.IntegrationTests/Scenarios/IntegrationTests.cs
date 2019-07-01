using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Xunit;

namespace ferrilata_devilline.IntegrationTests.Scenarios
{
    [Collection("BaseCollection")]
    public class IntegrationTests
    {
        private readonly TestContext _testContext;

        public IntegrationTests(TestContext testContext)
        {
            _testContext = testContext;
        }

        [Theory]
        [InlineData("api/post/pitch")]
        public async Task PostPitchCorrect_AuthorizationPresent(string url)
        {
            var newPosting = new Pitch
            {
                BadgeName = "English speaker",
                OldLVL = 2,
                PitchedLVL = 3,
                PitchMessage = "Hello World! My English is bloody gorgeous.",
                Holders = new[] { "balazs.jozsef", "benedek.vamosi", "balazs.barna" }.ToList()
            };

            var PostingJson = JsonConvert.SerializeObject(newPosting);

            var client = _testContext.Client;

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "something");

            var response = await client.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("api/post/pitch")]
        public async Task PostPitchCorrect_AuthorizationMissing(string url)
        {
            var newPosting = new Pitch
            {
                BadgeName = "English speaker",
                OldLVL = 2,
                PitchedLVL = 3,
                PitchMessage = "Hello World! My English is bloody gorgeous.",
                Holders = new[] { "balazs.jozsef", "benedek.vamosi", "balazs.barna" }.ToList()
            };

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
            var newPosting = new Pitch
            {
                BadgeName = "English speaker",
                OldLVL = 2,
                PitchedLVL = 3,
                PitchMessage = "Hello World! My English is bloody gorgeous.",

            };

            var PostingJson = JsonConvert.SerializeObject(newPosting);

            var client = _testContext.Client;

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "something");

            var response = await client.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("api/post/pitch")]
        public async Task PostPitchOKPropertytAuthorizationOK_TestMessage(string url)
        {
            var newPosting = new Pitch
            {
                BadgeName = "English speaker",
                OldLVL = 2,
                PitchedLVL = 3,
                PitchMessage = "Hello World! My English is bloody gorgeous.",
                Holders = new[] { "balazs.jozsef", "benedek.vamosi", "balazs.barna" }.ToList()
            };

            var PostingJson = JsonConvert.SerializeObject(newPosting);

            var client = _testContext.Client;

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "something");

            var response = await client.SendAsync(request);

            string ResponseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal("Created",
                JsonConvert.DeserializeObject<Dictionary<string, string>>(ResponseBody)["message"]);
        }

        [Theory]
        [InlineData("api/post/pitch")]
        public async Task PostPitchOKPropertytAuthorizationOK_TestUnauthorized(string url)
        {
            var newPosting = new Pitch
            {
                BadgeName = "English speaker",
                OldLVL = 2,
                PitchedLVL = 3,
                PitchMessage = "Hello World! My English is bloody gorgeous.",
                Holders = new[] { "balazs.jozsef", "benedek.vamosi", "balazs.barna" }.ToList()
            };

            var PostingJson = JsonConvert.SerializeObject(newPosting);

            var client = _testContext.Client;

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");

            var response = await client.SendAsync(request);

            string ResponseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal("Unauthorized",
                JsonConvert.DeserializeObject<Dictionary<string, string>>(ResponseBody)["message"]);
        }

        [Theory]
        [InlineData("api/post/pitch")]
        public async Task PostPitchOKPropertytAuthorizationOK_TestMissingField(string url)
        {
            var newPosting = new Pitch
            {
                BadgeName = "English speaker",
                OldLVL = 2,
                PitchedLVL = 3,
                PitchMessage = "Hello World! My English is bloody gorgeous.",
            };

            var PostingJson = JsonConvert.SerializeObject(newPosting);

            var client = _testContext.Client;

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "something");

            var response = await client.SendAsync(request);

            string ResponseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal("Please provide all fields",
                JsonConvert.DeserializeObject<Dictionary<string, string>>(ResponseBody)["error"]);
        }
    }
}
