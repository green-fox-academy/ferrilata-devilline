using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DAOs;
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
        [InlineData("api/pitch")]
        public async Task PutPitchApi_CorrectAuthentication_InCorrectBody_ShouldReturnNotFound(string url)
        {
            //Arrange
            var InputPitch = new Pitch {  };
            string InputJson = JsonConvert.SerializeObject(InputPitch);

            //Act
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Content = new StringContent(InputJson,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "test");
            var response = await _testContext.Client.SendAsync(request);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        //DAOs

        [Theory]
        [InlineData("api/pitch")]
        public async Task PutPitchApi_CorrectAuthentication_IncorrectBody_ShouldReturn_ErrorMessage(string url)
        {
            //Arrange
            var InputPitch = new Pitch {  };
            string InputJson = JsonConvert.SerializeObject(InputPitch);

            //Act
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Content = new StringContent(InputJson,
                                    Encoding.UTF8,
                                    "application/json");
            request.Headers.Add("Authorization", "test");
            var response = await _testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(JsonConvert.SerializeObject(new { error = "Please provide all fields" }), responseString);
        }

        public Pitch CreateNewPitch()
        {
            Pitch NewPitch = new Pitch {  };

            return NewPitch;
        }
    }
}
