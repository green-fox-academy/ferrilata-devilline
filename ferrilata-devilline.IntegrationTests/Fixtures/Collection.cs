using Xunit;

namespace ferrilata_devilline.IntegrationTests.Fixtures
{
    [CollectionDefinition("BaseCollection")]
    public class Collection : ICollectionFixture<TestContext>
    {
    }
}