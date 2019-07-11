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

        [Fact]
        public async Task Returns_CorrenctNrOfObjects()
        {
            var response = await _testContext.Client.GetAsync(_url);
            var content = await HtmlReader.GetDocumentAsync(response);
            var badgeList = content.QuerySelectorAll(".badgeObject");
            var levelList = content.QuerySelectorAll(".level");
            var holderList = content.QuerySelectorAll(".holder");

            Assert.Equal(2, badgeList.Length);
            Assert.Equal(3, levelList.Length);
            Assert.Equal(4, holderList.Length);
        }

        [Fact]
        public async Task Returns_CorrectFieldsOfObjects()
        {
            var response = await _testContext.Client.GetAsync(_url);
            var content = await HtmlReader.GetDocumentAsync(response);
            var firstBadgeVersion = content.QuerySelector(".badge td");
            var firstLevelNumber = content.QuerySelector(".level td");
            var firstHolderName = content.QuerySelector(".holder td");

            Assert.Equal("2.3", firstBadgeVersion.TextContent);
            Assert.Equal("1", firstLevelNumber.TextContent);
            Assert.Equal("balazs.barna", firstHolderName.TextContent);
        }
    }
}