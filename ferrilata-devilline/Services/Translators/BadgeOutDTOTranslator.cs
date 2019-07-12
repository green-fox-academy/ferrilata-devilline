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

        public List<BadgeOutDTO> Translate(List<Badge> badges)
        {
            List<BadgeOutDTO> resultBadges = new List<BadgeOutDTO>();
            foreach (Badge badge in badges)
            {
                List<LevelOutDTO> resultLevels = new List<LevelOutDTO>();

                List<Level> levels = _context.Levels
                    .Where(l => l.Badge.BadgeId == badge.BadgeId)
                    .ToList();

                foreach (Level level in levels)
                {
                    List<PersonDTO> resultHolders = new List<PersonDTO>();

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

        private PersonDTO TranslateToHolder(User user)
        {
            return new PersonDTO
            {
                PersonId = user.UserId,
                Name = user.Name
            };
        }

        private LevelOutDTO TranslateToLevelDTO(Level level, List<PersonDTO> resultHolders)
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

        private BadgeOutDTO TranslateToBadgeDTO(Badge badge, List<LevelOutDTO> resultLevels)
        {
            return new BadgeOutDTO
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
