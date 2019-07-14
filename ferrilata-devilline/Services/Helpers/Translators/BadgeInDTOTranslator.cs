using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;

namespace ferrilata_devilline.Services.Translators
{
    public class BadgeInDTOTranslator
    {
        public Badge TranslateBadge(JToken jToken)
        {
            var incomingBadge = jToken.ToObject<BadgeInDTO>();

            return new Badge
            {
                Version = incomingBadge.Version,
                Name = incomingBadge.Name,
                Tag = incomingBadge.Tag
            };
        }

        public List<Level> ExtractLevels(JToken jToken, Badge badge)
        {
            var result = new List<Level> { };
            var incomingBadge = jToken.ToObject<BadgeInDTO>();

            foreach (LevelInDTO incomingLevel in incomingBadge.Levels)
            {
                var newLevel = new Level
                {
                    LevelNumber = incomingLevel.LevelNumber,
                    Description = incomingLevel.Description,
                    Weight = incomingLevel.Weight,
                    Badge = badge,
                    UserLevels = new List<UserLevel> { }
                };

                result.Add(newLevel);
            }

            return result;
        }
    }
}
