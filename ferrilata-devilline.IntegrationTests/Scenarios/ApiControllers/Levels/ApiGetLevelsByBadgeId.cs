using System;
using ferrilata_devilline.IntegrationTests.Fixtures;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;
using ferrilata_devilline.Models.DAOs;

namespace ferrilata_devilline.IntegrationTests.Scenarios.ApiControllers.Levels
{
    [Collection("BaseCollection")]
    public class ApiGetLevelsByBadgeId
    {
        private readonly TestContext _testContext;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        private string token;

        public ApiGetLevelsByBadgeId(TestContext testContext)
        {
            _testContext = testContext;
            _tokenService = _testContext.TokenService;
            token = "Bearer " + _tokenService.GenerateToken("useremail@ferillata.com", true);
            _mapper = testContext.testMapper;
        }

        [Fact]
        public async Task GetLevelsByBadgeIdApi_CorrectAuthentication_ShouldReturnOK()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token);
            var response = await _testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetLevelsByBadgeIdApi_CorrectAuthentication_ShouldReturn_BodyTypeBadgeDTO()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token);
            var response = await _testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<LevelOutDTO>>(responseString);
            Assert.True(actual.GetType() == typeof(List<LevelOutDTO>));
        }

        [Fact]
        public async Task GetLevelsByBadgeIdApi_CorrectAuthentication_ShouldReturn_BadgeLevels()
        {
            var BadgeLevels = _testContext
                .Context
                .Badges
                .Include(b => b.Levels)
                .ToList().Find(b => b.BadgeId == 1).Levels;
            var BadgeLevelsOutDTO = _mapper.Map<List<Level>, List<LevelOutDTO>>(BadgeLevels);
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges/1/levels");
            request.Headers.Add("Authorization", token);
            var response = await _testContext.Client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<LevelOutDTO>>(responseString);
            for (int i = 0; i < actual.Count(); i++)
            {
                Assert.True(actual[i].Equals(BadgeLevelsOutDTO[i]));
            }
        }

        [Fact]
        public async Task GetBadgesByIdApi_InCorrectAuthentication_ShouldReturnUnauthorized()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges/1/levels");
            var response = await _testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}

