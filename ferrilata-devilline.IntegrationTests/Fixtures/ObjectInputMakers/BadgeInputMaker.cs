using ferrilata_devilline.IntegrationTests.Fixtures.Models;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Models.DTOs.Input;
using System.Collections.Generic;

namespace ferrilata_devilline.Services.Helpers.Extensions.ObjectTypeCheckers.ObjectInputMakers
{
    public static class BadgeInputMaker
    {
        public static BadgeInDTO MakeCorrect()
        {
            var levels = new List<LevelInDTO>
            {
                new LevelInDTO
                {
                    LevelNumber = 3,
                    Weight = "heavy",
                    Description = "to be described"
                }
            };

            return new BadgeInDTO
            {
                Version = 2.2,
                Name = "such name",
                Tag = "such tag",
                Levels = levels
            };
        }

        public static BadgeDTO MakeCorrectBadgeDTO()
        {
            var levels = new List<LevelWithoutHoldersDTO>
            {
                new LevelWithoutHoldersDTO
                {
                    LevelId = 1,
                    LevelNumber = 3,
                    Weight = "heavy",
                    Description = "to be described"
                }
            };

            return new BadgeDTO
            {
                BadgeId = 1,
                Version = 2.2,
                Name = "such name",
                Tag = "such tag",
                Levels = levels
            };
        }

        public static BadgeDTOWithNullValues MakeBadgeDTOWithNullValue()
        {
            return new BadgeDTOWithNullValues
            {
                BadgeId = 1,
                Version = 2.2,
                Name = "such name",
                Tag = "such tag",
                Levels = null
            };
        }
    }
}
