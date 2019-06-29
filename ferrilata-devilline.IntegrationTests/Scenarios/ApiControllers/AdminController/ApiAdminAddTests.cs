using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ferrilata_devilline.IntegrationTests.Fixtures;

namespace ferrilata_devilline.IntegrationTests.Scenarios
{
    [Collection("BaseCollection")]
    public class ApiAdminAddTests
    {
        private readonly TestContext _testContext;
        private readonly HttpContent CorrectRequestContent;
        private readonly HttpContent IncorrectRequestContent;

        public ApiAdminAddTests(TestContext testContext)
        {
            _testContext = testContext;

            var CorrectRequestBodyObject = new { version = "2.3", name = "Badge inserter", tag = "general", levels = new List<object>() };
            string correctRequestBody = JsonConvert.SerializeObject(CorrectRequestBodyObject);
            this.CorrectRequestContent = new StringContent(correctRequestBody, Encoding.UTF8, "application/json");

            var IncorrectRequestBodyObject = new { version = "2.3", levels = new List<object>() };
            string IncorrectRequestBody = JsonConvert.SerializeObject(IncorrectRequestBodyObject);
            this.IncorrectRequestContent = new StringContent(IncorrectRequestBody, Encoding.UTF8, "application/json");
        }

        [Fact]
        public async Task Authorized_AndHasCorrectBody_Created()
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "/api/admin/add");
            message.Headers.Add("Authorization", "something");
            message.Content = CorrectRequestContent;

            var response = await _testContext.Client.SendAsync(message);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Authorized_AndHasCorrectBody_ResponseObject()
        {
            var expectedResponseObject = new List<object>() { new { message = "Created" } };
            string expectedResponseString = JsonConvert.SerializeObject(expectedResponseObject);
            HttpContent expectedResponse = new StringContent(expectedResponseString, Encoding.UTF8, "application/json");
            string expected = await expectedResponse.ReadAsStringAsync();

            var message = new HttpRequestMessage(HttpMethod.Post, "/api/admin/add");
            message.Headers.Add("Authorization", "something");
            message.Content = CorrectRequestContent;

            var response = await _testContext.Client.SendAsync(message);
            string received = await response.Content.ReadAsStringAsync();

            Assert.Equal(expected, received);
        }

        [Fact]
        public async Task Authorized_AndHasIncorrectBody_BadRequest()
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "/api/admin/add");
            message.Headers.Add("Authorization", "something");
            message.Content = IncorrectRequestContent;

            var response = await _testContext.Client.SendAsync(message);

            Assert.Equal("BadRequest", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Authorized_AndHasIncorrectBody_ResponseObject()
        {
            var expectedResponseObject = new { error = "Please provide all fields" };
            string expectedResponseString = JsonConvert.SerializeObject(expectedResponseObject);
            HttpContent expectedResponse = new StringContent(expectedResponseString, Encoding.UTF8, "application/json");
            string expected = await expectedResponse.ReadAsStringAsync();

            var message = new HttpRequestMessage(HttpMethod.Post, "/api/admin/add");
            message.Headers.Add("Authorization", "something");
            message.Content = IncorrectRequestContent;

            var response = await _testContext.Client.SendAsync(message);
            string received = await response.Content.ReadAsStringAsync();

            Assert.Equal(expected, received);
        }

        [Fact]
        public async Task AuthorizationFieldIsEmpty_Unauthorized()
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "/api/admin/add");
            message.Headers.TryAddWithoutValidation("Authorization", "");
            message.Content = IncorrectRequestContent;

            var response = await _testContext.Client.SendAsync(message);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task AuthorizationFieldIsMissing_Unauthorized()
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "/api/admin/add");
            message.Content = IncorrectRequestContent;

            var response = await _testContext.Client.SendAsync(message);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }                    
    }
}