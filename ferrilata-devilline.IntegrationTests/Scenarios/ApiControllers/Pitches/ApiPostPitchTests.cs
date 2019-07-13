using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.IntegrationTests.Fixtures.Models;
using ferrilata_devilline.IntegrationTests.Fixtures.ObjectInputMakers;
using ferrilata_devilline.Models.DTOs;

namespace ferrilata_devilline.IntegrationTests.Scenarios
{
    [Collection("BaseCollection")]
    public class ApiPostPitchTests
    {
        private readonly TestContext _testContext;
        private readonly PitchInDTO _correctPitch;
        private readonly PitchInDTOWithNullValues _inCorrectPitch;

        public ApiPostPitchTests()
        {
            _testContext = new TestContext();
            _correctPitch = PitchInputMaker.MakeCorrectPitchInDTO();
            _inCorrectPitch = PitchInputMaker.MakeInCorrectPitchInDTO();
        }

        [Theory]
        [InlineData("api/post/pitch")]
        public async Task PostPitchCorrect_AuthorizationPresent(string url)
        {
            string PostingJson = JsonConvert.SerializeObject(_correctPitch);

            var client = _testContext.Client;
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "something");
            var response = await client.SendAsync(request);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData("api/post/pitch")]
        public async Task PostPitchOKProperty_AuthorizationOK_TestMessage(string url)
        {

            string PostingJson = JsonConvert.SerializeObject(_correctPitch);

            var client = _testContext.Client;
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "something");
            var response = await client.SendAsync(request);
            string ResponseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(new { message = "Created" }), ResponseBody);
        }

        [Theory]
        [InlineData("api/post/pitch")]
        public async Task PostPitchOKPropertytAuthorizationOK_PitchSaved(string url)
        {
            string PostingJson = JsonConvert.SerializeObject(_correctPitch);

            var client = _testContext.Client;
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "something");
            await client.SendAsync(request);

            Assert.Equal(3, _testContext.Context.Pitches.Count());
            Assert.Equal("result", _testContext.Context.Pitches.ToArray()[2].Result);
        }

        [Theory]
        [InlineData("api/post/pitch")]
        public async Task PostPitchCorrect_AuthorizationMissing(string url)
        {
            var PostingJson = JsonConvert.SerializeObject(_correctPitch);

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
        public async Task PostPitchMissingProperty_AuthorizationOK_NotFound(string url)
        {
            string PostingJson = JsonConvert.SerializeObject(_inCorrectPitch);

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
        public async Task PostPitchMissingProperty_AuthorizationOK_Message(string url)
        {

            string PostingJson = JsonConvert.SerializeObject(_inCorrectPitch);

            var client = _testContext.Client;
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "something");
            var response = await client.SendAsync(request);
            string ResponseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(new { error = "Please provide all fields" }),
              ResponseBody);
        }

        [Theory]
        [InlineData("api/post/pitch")]
        public async Task PostPitchOKProperty_AuthorizationMissing_TestUnauthorized(string url)
        {

            string PostingJson = JsonConvert.SerializeObject(_correctPitch);

            var client = _testContext.Client;
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(PostingJson,
                                    Encoding.UTF8,
                                    "application/json");
            var response = await client.SendAsync(request);
            string ResponseBody = await response.Content.ReadAsStringAsync();

            Assert.Equal(JsonConvert.SerializeObject(new { message = "Unauthorized" }), ResponseBody);
        }
    }
}
