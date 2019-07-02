using ferrilata_devilline.IntegrationTests.Fixtures;
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
    }
}