using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ferrilata_devilline.Models.DTOs;

namespace ferrilata_devilline.Services
{
    public class BadgeService : IBadgeService
    {
        readonly ApplicationContext _context;

        public BadgeService(ApplicationContext context)
        {
            _context = context;
        }

        public List<BadgeDTO> GetAndTranslateAll()
        {
            List<BadgeDTO> resultBadges = new List<BadgeDTO>();

            List<Badge> badges = _context.Badges.ToList();
            foreach (Badge badge in badges)
            {
                List<LevelDTO> resultLevels = new List<LevelDTO>();

                List<Level> levels = _context.Levels
                    .Where(l => l.Badge.BadgeId == badge.BadgeId)
                    .ToList();

                foreach (Level level in levels)
                {
                    List<HolderDTO> resultHolders = new List<HolderDTO>();

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

        private HolderDTO TranslateToHolder(User user)
        {
            return new HolderDTO
            {
                HolderId = user.UserId,
                Name = user.Name
            };
        }

        private LevelDTO TranslateToLevelDTO(Level level, List<HolderDTO> resultHolders)
        {
            return new LevelDTO
            {
                LevelId = level.LevelId,
                LevelNumber = level.LevelNumber,
                Weight = level.Weight,
                Description = level.Description,
                Holders = resultHolders
            };
        }

        private BadgeDTO TranslateToBadgeDTO(Badge badge, List<LevelDTO> resultLevels)
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