using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Services.Helpers.Extensions.ObjectTypeCheckers.ObjectInputMakers;

namespace ferrilata_devilline.IntegrationTests.Scenarios
{
    [Collection("BaseCollection")]
    public class ApiPutPitchTests
    {
        private readonly TestContext _testContext;
        private readonly string _correctPitchString;
        private readonly string _inCorrectPitchString;

        public ApiPutPitchTests()
        {
            _testContext = new TestContext(); 
            _correctPitchString = JsonConvert.SerializeObject(PitchInputMaker.MakeCorrectPitchDTO());
            _inCorrectPitchString = JsonConvert.SerializeObject(PitchInputMaker.MakeCorrectPitchInDTO());
        }

        [Theory]
        [InlineData("api/pitch")]
        public async Task PutPitchApi_CorrectAuthentication_CorrectBody_ShouldReturnOK(string url)
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Content = new StringContent(_correctPitchString,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "test");

            //Act
            var response = await _testContext.Client.SendAsync(request);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("api/pitch")]
        public async Task PutPitchApi_CorrectAuthentication_CorrectBody_ShouldReturnSuccess(string url)
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Content = new StringContent(_correctPitchString,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "test");

            //Act
            var response = await _testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(JsonConvert.SerializeObject(new { message = "Success" }), responseString);
        }

        [Theory]
        [InlineData("api/pitch")]
        public async Task PutPitchApi_CorrectAuthentication_CorrectBody_UpdatesInDatabase(string url)
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Content = new StringContent(_correctPitchString,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "test");

            //Act
            await _testContext.Client.SendAsync(request);

            string updatedResult = _testContext.Context.Pitches
                .Where(p => p.PitchId == 1)
                .FirstOrDefault()
                .Result;

            //Assert
            Assert.Equal(2, _testContext.Context.Pitches.Count());
            Assert.Equal("result updated", updatedResult);
        }

        [Theory]
        [InlineData("api/pitch")]
        public async Task PutPitchApi_IncorrectAuthentication_CorrectBody_ShouldReturnUnauthorized(string url)
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Content = new StringContent(_correctPitchString,
                                    Encoding.UTF8,
                                    "application/json");
            //Act
            var response = await _testContext.Client.SendAsync(request);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData("api/pitch")]
        public async Task PutPitchApi_IncorrectAuthentication_CorrectBody_ShouldReturnMessage_Unauthorized(string url)
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Content = new StringContent(_correctPitchString,
                                    Encoding.UTF8,
                                    "application/json");
            //Act
            var response = await _testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(JsonConvert.SerializeObject(new { error = "Unauthorized" }), responseString);
        }

        [Theory]
        [InlineData("api/pitch")]
        public async Task PutPitchApi_CorrectAuthentication_InCorrectBody_ShouldReturnNotFound(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Content = new StringContent(_inCorrectPitchString,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "test");
            var response = await _testContext.Client.SendAsync(request);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("api/pitch")]
        public async Task PutPitchApi_CorrectAuthentication_IncorrectBody_ShouldReturn_ErrorMessage(string url)
        {
            //Arrange
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Content = new StringContent(_inCorrectPitchString,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "test");

            //Act
            var response = await _testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(JsonConvert.SerializeObject(new { error = "Please provide all fields" }), responseString);
        }
    }
}