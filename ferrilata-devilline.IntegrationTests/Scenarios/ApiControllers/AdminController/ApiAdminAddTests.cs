using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.IntegrationTests.Fixtures.Models;
using ferrilata_devilline.Services.Interfaces;
using ferrilata_devilline.Services.Helpers.Extensions.ObjectTypeCheckers.ObjectInputMakers;

namespace ferrilata_devilline.IntegrationTests.Scenarios
{
    [Collection("BaseCollection")]
    public class ApiAdminAddTests
    {
        private readonly TestContext _testContext;
        private readonly HttpRequestMessage _message;
        private readonly ITokenService _tokenService;
        private readonly string email;

        public ApiAdminAddTests(TestContext testContext)
        {
            _testContext = testContext;
            _message = new HttpRequestMessage(HttpMethod.Post, "/api/admin/add");
            _tokenService = _testContext.TokenService;
            email = "useremail@ferillata.com";
        }

        [Theory]
        [MemberData(nameof(Correct))]
        public async Task Authorized_AndHasCorrectBody_Created(HttpContent content)
        {
            _message.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            _message.Content = content;

            var response = await _testContext.Client.SendAsync(_message);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(Correct))]
        public async Task Authorized_AndHasCorrectBody_ResponseObject(HttpContent content)
        {
            var expectedResponseObject = new List<object>() { new { message = "Created" } };
            string expected = JsonConvert.SerializeObject(expectedResponseObject);

            _message.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            _message.Content = content;

            var response = await _testContext.Client.SendAsync(_message);
            string received = await response.Content.ReadAsStringAsync();

            Assert.Equal(expected, received);
        }

        [Theory]
        [MemberData(nameof(Correct))]
        public async Task AuthorizationFieldIsEmpty_Unauthorized(HttpContent content)
        {
            _message.Headers.TryAddWithoutValidation("Authorization", "");
            _message.Content = content;

            var response = await _testContext.Client.SendAsync(_message);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(Correct))]
        public async Task AuthorizationFieldIsMissing_Unauthorized(HttpContent content)
        {
            _message.Content = content;

            var response = await _testContext.Client.SendAsync(_message);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(MissingFields))]
        [MemberData(nameof(NullValue))]
        public async Task Authorized_IncorrectBody_BadRequest(HttpContent content)
        {
            _message.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            _message.Content = content;

            var response = await _testContext.Client.SendAsync(_message);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(MissingFields))]
        [MemberData(nameof(NullValue))]
        public async Task Authorized_IncorrectBody_ResponseObject(HttpContent content)
        {
            var expectedResponseObject = new { error = "Please provide all fields" };
            string expected = JsonConvert.SerializeObject(expectedResponseObject);

            _message.Headers.Add("Authorization", "Bearer " + _tokenService.GenerateToken(email, true));
            _message.Content = content;

            var response = await _testContext.Client.SendAsync(_message);
            string received = await response.Content.ReadAsStringAsync();

            Assert.Equal(expected, received);
        }

        public static IEnumerable<object[]> MissingFields =>
            new List<object[]>()
            {
                new object[]
                {   new StringContent(JsonConvert.SerializeObject(
                        new
                        {
                            version = "2.3", levels = new List<object>()
                        } ), Encoding.UTF8, "application/json")
                },
            };


        public static IEnumerable<object[]> NullValue =>
            new List<object[]>()
            {
                new object[]
                {
                    new StringContent(
                        JsonConvert.SerializeObject(
                            BadgeInputMaker.MakeWithNullValue()
                        ), Encoding.UTF8, "application/json"
                    )
                }
            };

        public static IEnumerable<object[]> Correct =>
            new List<object[]>()
            {
                new object[]
                {
                    new StringContent(
                        JsonConvert.SerializeObject(
                            BadgeInputMaker.MakeCorrect()
                        ), Encoding.UTF8, "application/json"
                    )
                }
            };
    }
}