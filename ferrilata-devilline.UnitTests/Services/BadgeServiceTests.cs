using System.Collections.Generic;
using AutoMapper;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services;
using Moq;
using Xunit;

namespace ferrilata_devilline.UnitTests.Services
{
    public class BadgeServiceTests
    {
        private readonly Mock<IBadgeRepository> _mockBadgeRepo;
        private readonly Mock<ILevelRepository> _mockLevelRepo;
        private readonly Mock<IMapper> _mockMapper;

        public BadgeServiceTests()
        {
            _mockBadgeRepo = new Mock<IBadgeRepository>();
            _mockLevelRepo = new Mock<ILevelRepository>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public void BadgesService_ShouldReturnAllBadges_WhenGetAllMethodIsCalled()
        {
            _mockBadgeRepo.Setup(repo => repo.RetrieveBadgesFromDB())
                .Returns(GetTestBadges());
            var service = new BadgeService(_mockBadgeRepo.Object, _mockMapper.Object, _mockLevelRepo.Object);
            var expected = GetTestBadges();
            var actual = service.GetAll();
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void BadgesService_ShouldReturnBadgeWithSpecifiedId_WhenGetBadgeByIdMethodIsCalled(long id)
        {
            _mockBadgeRepo.Setup(repo => repo.FindBadgeById(id))
                .Returns(GetTestBadges().Find(b => b.BadgeId == id));
            var service = new BadgeService(_mockBadgeRepo.Object, _mockMapper.Object, _mockLevelRepo.Object);
            var expected = GetTestBadges().Find(b => b.BadgeId == id);
            var actual = service.FindBadgeById(id);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void BadgesService_ShouldDeleteBadgeWithSpecifiedId_WhenDeleteByIdMethodIsCalled(long id)
        {
            var service = new BadgeService(_mockBadgeRepo.Object, _mockMapper.Object, _mockLevelRepo.Object);
            service.DeleteById(id);

            _mockBadgeRepo.Verify(m => m.DeleteBadgeById(id), Times.Once);
        }

        private static List<Badge> GetTestBadges()
        {
            var badges = new List<Badge>
            {
                new Badge
                {
                    BadgeId = 1,
                    Version = 1.0,
                    Name = "English speaker",
                    Tag = "languages",
                    Levels = new List<Level>()
                },

                new Badge
                {
                    BadgeId = 1,
                    Version = 1.0,
                    Name = "English speaker",
                    Tag = "languages",
                    Levels = new List<Level>()
                }
            };

            return badges;
        }
    }
}