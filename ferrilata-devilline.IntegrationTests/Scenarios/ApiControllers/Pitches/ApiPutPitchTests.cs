using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ferrilata_devilline.IntegrationTests.Scenarios
{
    [Collection("BaseCollection")]
    public class ApiPutPitchTests
    {
        private readonly TestContext _testContext;
        public ApiPutPitchTests(TestContext testContext)
        {
            _testContext = testContext;
        }

        [Theory]
        [InlineData("api/put/pitch")]
        public async Task PutPitchCorrect_Authorization(string url)
        {
            //Arrange
            var InputPitch = CreateNewPitch();
            string InputJson = JsonConvert.SerializeObject(InputPitch);
            var client = _testContext.Client;

            //Act
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Content = new StringContent(InputJson,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "test");
            var response = await client.SendAsync(request);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }


        public Pitch CreateNewPitch()
        {
            Pitch NewPitch = new Pitch { Username = "UserName", BadgeName = "BadgeName", Status = "status", PitchMessage = "level" };

            return NewPitch;
        }
    }
}
