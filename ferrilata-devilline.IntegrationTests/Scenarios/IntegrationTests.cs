using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ferrilata_devilline.IntegrationTests.Fixtures;
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
        
    }
}