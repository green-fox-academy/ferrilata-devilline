using ferrilata_devilline.IntegrationTests.Fixtures;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ferrilata_devilline.IntegrationTests
{
    [Collection("BaseCollection")]
    public class ApiAdminTests
    {
        private readonly TestContext testContext;
        private readonly HttpClient httpClient;


        public ApiAdminTests(TestContext testContext)
        {
            this.testContext = testContext;
           
        }

        [Fact]
        public async Task BadgesApi_CorrectAuthentication_ShouldReturnOK()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            request.Headers.Add("Authorization", "test");
            var response = await testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task BadgesApi_AuthorizationHeader_IsMissing_ShouldReturnUnathorized()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
            //request.Headers.Add("Authorization", "");
            var response = await testContext.Client.SendAsync(request);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        //[Fact]
        //public async Task BadgesApi_AuthorizationHeader_IsMissing_ShouldReturnUnathorized()
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
        //    //request.Headers.Add("Authorization", "");
        //    var response = await testContext.Client.SendAsync(request);
        //    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        //}


    }
}
