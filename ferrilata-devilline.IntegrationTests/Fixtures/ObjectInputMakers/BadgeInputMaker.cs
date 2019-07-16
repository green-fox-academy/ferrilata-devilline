using ferrilata_devilline.IntegrationTests.Fixtures.Models;
using ferrilata_devilline.Models.DTOs;
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

        public static BadgeInDTOWithNullValues MakeWithNullValue()
        {
            return new BadgeInDTOWithNullValues
            {
                Version = 2.2,
                Name = "such name",
                Tag = "such tag",
                Levels = null
            };
        }
    }
}
