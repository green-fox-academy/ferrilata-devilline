using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models;
using Newtonsoft.Json;
using Xunit;

namespace ferrilata_devilline.IntegrationTests.Scenarios
{
    [Collection("BaseCollection")]
    public class ApiPitchesIntegrationTests
    {
        private const string ApiPitches = "/api/pitches";
        private readonly TestContext _testContext;

        public ApiPitchesIntegrationTests(TestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task PitchesApi_Should_Return401_WhenRequestHeaderAuthorizationIsMissing()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, ApiPitches);

            var response = await _testContext.Client.SendAsync(request);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task PitchesApi_Should_ReturnErrorMessage_WhenRequestHeaderAuthorizationIsMissing()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, ApiPitches);
            var expected = new Error("Unauthorized");

            var response = await _testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<Error>(responseString);

            Assert.Equal(actual, expected);
        }

        [Fact]
        public async Task PitchesApi_Should_ReturnStatusOK_WhenRequestHeaderAuthorizationIsNotEmpty()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, ApiPitches);
            request.Headers.Add("Authorization", "something");

            var response = await _testContext.Client.SendAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PitchesApi_Should_ReturnJSONPitches_WhenRequestHeaderAuthorizationIsNotEmpty()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, ApiPitches);
            request.Headers.Add("Authorization", "something");

            var response = await _testContext.Client.SendAsync(request);

            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<Pitches>(responseString);
            Assert.Equal(typeof(Pitches), actual.GetType());
        }
    }
}