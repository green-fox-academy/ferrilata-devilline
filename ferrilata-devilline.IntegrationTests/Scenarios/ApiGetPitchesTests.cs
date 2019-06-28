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
    public class IntegrationTests
    {
        private readonly TestContext _testContext;

        public IntegrationTests(TestContext testContext)
        {
            _testContext = testContext;
        }

        [Theory]
        [InlineData("/api/pitches")]
        public async Task PitchesApi_Should_Return401_WhenRequestHeaderAuthorizationIsMissing(string url)
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            //Act
            var response = await _testContext.Client.SendAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/pitches")]
        public async Task PitchesApi_Should_ReturnErrorMessage_WhenRequestHeaderAuthorizationIsMissing(string url)
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var expected = new Error("Unauthorizied");

            //Act
            var response = await _testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<Error>(responseString);

            //Assert
            Assert.True(actual.Equals(expected));
        }

        [Theory]
        [InlineData("/api/pitches")]
        public async Task PitchesApi_Should_ReturnStatusOK_WhenRequestHeaderAuthorizationIsNotEmpty(string url)
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", "something");

            //Act
            var response = await _testContext.Client.SendAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/pitches")]
        public async Task PitchesApi_Should_ReturnJSONPitchers_WhenRequestHeaderAuthorizationIsNotEmpty(string url)
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", "something");

            //Act
            var response = await _testContext.Client.SendAsync(request);

            //Assert
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<Pitches>(responseString);
            Assert.True(actual.GetType() == typeof(Pitches));
        }
    }
}