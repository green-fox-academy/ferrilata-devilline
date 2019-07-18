using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Services.Helpers.Extensions.ObjectTypeCheckers.ObjectInputMakers;

namespace ferrilata_devilline.IntegrationTests.Scenarios
{
    [Collection("BaseCollection")]
    public class ApiPutBadgesTests
    {
        private readonly TestContext _testContext;
        private readonly HttpRequestMessage _message;

        public ApiPutBadgesTests()
        {
            _testContext = new TestContext();
            _message = new HttpRequestMessage(HttpMethod.Put, "/api/badges/1");
        }

        [Theory]
        [MemberData(nameof(Correct))]
        public async Task Authorized_AndHasCorrectBody_Created(HttpContent content)
        {
            _message.Headers.Add("Authorization", "something");
            _message.Content = content;

            var response = await _testContext.Client.SendAsync(_message);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(Correct))]
        public async Task Authorized_AndHasCorrectBody_ResponseObject(HttpContent content)
        {
            var expectedResponseObject = new { message = "Created" };
            string expected = JsonConvert.SerializeObject(expectedResponseObject);

            _message.Headers.Add("Authorization", "something");
            _message.Content = content;

            var response = await _testContext.Client.SendAsync(_message);
            string received = await response.Content.ReadAsStringAsync();

            Assert.Equal(expected, received);
        }

        [Theory]
        [MemberData(nameof(Correct))]
        public async Task Authorized_AndHasCorrectBody_BadgeUpdated(HttpContent content)
        {
            var expectedResponseObject = new List<object>() { new { message = "Created" } };
            string expected = JsonConvert.SerializeObject(expectedResponseObject);
            _message.Headers.Add("Authorization", "something");
            _message.Content = content;

            await _testContext.Client.SendAsync(_message);

            var updatedTag = _testContext.Context.Badges.Where(b => b.BadgeId.Equals(1)).FirstOrDefault().Tag;
            Assert.Equal("such tag", updatedTag);
        }
          
        [Theory]
        [MemberData(nameof(Correct))]
        public async Task Authorized_AndHasCorrectBody_LevelUpdated(HttpContent content)
        {
            var expectedResponseObject = new List<object>() { new { message = "Created" } };
            string expected = JsonConvert.SerializeObject(expectedResponseObject);
            _message.Headers.Add("Authorization", "something");
            _message.Content = content;

            await _testContext.Client.SendAsync(_message);

            var updatedDescription = _testContext.Context.Levels.Where(b => b.LevelId.Equals(1)).FirstOrDefault().Description;
            Assert.Equal("to be described", updatedDescription);
        }

        [Theory]
        [MemberData(nameof(Correct))]
        public async Task Authorized_AndHasCorrectBody_BadgeAndLevelAreUpdated_AndConnected(HttpContent content)
        {
            var expectedResponseObject = new List<object>() { new { message = "Created" } };
            string expected = JsonConvert.SerializeObject(expectedResponseObject);

            _message.Headers.Add("Authorization", "something");
            _message.Content = content;

            await _testContext.Client.SendAsync(_message);

            Badge newBadge = _testContext.Context.Badges.Where(b => b.Tag.Equals("such tag")).FirstOrDefault();
            Level newLevel = _testContext.Context.Levels.Where(b => b.Weight.Equals("heavy")).FirstOrDefault();

            Assert.Equal(newBadge.BadgeId, newLevel.Badge.BadgeId);
        }

        [Theory]
        [MemberData(nameof(Correct))]
        public async Task Authorized_ContradictingId_ResponseBadRequest(HttpContent content)
        {
            var expectedResponseObject = new { message = "Please provide a single Badge ID" };
            string expected = JsonConvert.SerializeObject(expectedResponseObject);

            var messageWithIncorrectIdInURI = new HttpRequestMessage(HttpMethod.Put, "/api/badges/1");
            messageWithIncorrectIdInURI.Headers.Add("Authorization", "something");
            messageWithIncorrectIdInURI.Content = content;

            var response = await _testContext.Client.SendAsync(messageWithIncorrectIdInURI);
            string received = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
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
            _message.Headers.Add("Authorization", "something");
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

            _message.Headers.Add("Authorization", "something");
            _message.Content = content;

            var response = await _testContext.Client.SendAsync(_message);
            string received = await response.Content.ReadAsStringAsync();

            Assert.Equal(expected, received);
        }

        public static IEnumerable<object[]> MissingFields =>
            new List<object[]>()
            {
                new object[]
                {   new StringContent(
                        JsonConvert.SerializeObject(
                            new { }
                        ), Encoding.UTF8, "application/json"
                    )
                },
            };


        public static IEnumerable<object[]> NullValue =>
            new List<object[]>()
            {
                new object[]
                {
                    new StringContent(
                        JsonConvert.SerializeObject(
                            BadgeInputMaker.MakeBadgeDTOWithNullValue()
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
                            BadgeInputMaker.MakeCorrectBadgeDTO()
                        ), Encoding.UTF8, "application/json"
                    )
                }
            };
    }
}