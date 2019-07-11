using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Services.Translators
{
    public class BadgeOutDTOTranslator
    {
        readonly ApplicationContext _context;

        public BadgeOutDTOTranslator(ApplicationContext context)
        {
            _context = context;
        }

        public List<BadgeDTO> Translate(List<Badge> badges)
        {
            List<BadgeDTO> resultBadges = new List<BadgeDTO>();
            foreach (Badge badge in badges)
            {
                List<LevelOutDTO> resultLevels = new List<LevelOutDTO>();

                List<Level> levels = _context.Levels
                    .Where(l => l.Badge.BadgeId == badge.BadgeId)
                    .ToList();

                foreach (Level level in levels)
                {
                    List<HolderOutDTO> resultHolders = new List<HolderOutDTO>();

                    List<User> users = _context.Users
                          .Include("UserLevels")
                          .Where(u => u.UserLevels
                                       .Select(l => l.LevelId)
                                       .Contains(level.LevelId))
                          .ToList();

                    foreach (User user in users)
                    {
                        resultHolders.Add(TranslateToHolder(user));
                    }

                    resultLevels.Add(TranslateToLevelDTO(level, resultHolders));
                }

                resultBadges.Add(TranslateToBadgeDTO(badge, resultLevels));
            }

            return resultBadges;
        }

        private HolderOutDTO TranslateToHolder(User user)
        {
            return new HolderOutDTO
            {
                HolderId = user.UserId,
                Name = user.Name
            };
        }

        private LevelOutDTO TranslateToLevelDTO(Level level, List<HolderOutDTO> resultHolders)
        {
            return new LevelOutDTO
            {
                LevelId = level.LevelId,
                LevelNumber = level.LevelNumber,
                Weight = level.Weight,
                Description = level.Description,
                Holders = resultHolders
            };
        }

        private BadgeDTO TranslateToBadgeDTO(Badge badge, List<LevelOutDTO> resultLevels)
        {
            return new BadgeDTO
            {
                BadgeId = badge.BadgeId,
                Version = badge.Version,
                Name = badge.Name,
                Tag = badge.Tag,
                Levels = resultLevels
            };
        }
    }
}
