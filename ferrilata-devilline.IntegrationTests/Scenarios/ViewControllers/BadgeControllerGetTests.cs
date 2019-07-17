using ferrilata_devilline.IntegrationTests.Fixtures;
using System.Threading.Tasks;
using Xunit;

namespace ferrilata_devilline.IntegrationTests.Scenarios.ViewControllers
{
    [Collection("BaseCollection")]
    public class BadgeControllerGetTests
    {
        private readonly TestContext _testContext;
        private readonly string _url;

        public BadgeControllerGetTests(TestContext testContext)
        {
            _testContext = testContext;
            _url = "/badgelibrary";
        }

        [Fact]
        public async Task Returns_SuccessStatusCode_AndCorrectContentType()
        {
            var response = await _testContext.Client.GetAsync(_url);
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Returns_Layout()
        {
            var response = await _testContext.Client.GetAsync(_url);
            var content = await HtmlReader.GetDocumentAsync(response);
            var navbarsList = content.QuerySelectorAll(".navbar");

            Assert.Equal(1, navbarsList.Length);
        }       
    }
}