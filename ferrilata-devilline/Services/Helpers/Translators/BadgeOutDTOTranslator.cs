using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Repositories;

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
            var result = new List<BadgeOutDTO> { };
            foreach (Badge badge in badges)
            {
                var newBadgeDTO = TranslateToBadgeDTO(badge);
                result.Add(newBadgeDTO);
            }

            return result;
        }

        private BadgeOutDTO TranslateToBadgeDTO(Badge badge)
        {
            return new BadgeOutDTO
            {
                BadgeId = badge.BadgeId,
                Version = badge.Version,
                Name = badge.Name,
                Tag = badge.Tag,
                Levels = TranslateToLevelDTOList(
                    _context.Levels
                        .Where(l => l.Badge.BadgeId == badge.BadgeId)
                        .ToList()
                    )
            };
        }

        private List<LevelOutDTO> TranslateToLevelDTOList(List<Level> levels)
        {

            var result = new List<LevelOutDTO> { };
            foreach (Level level in levels)
            {
                var newLevelDTO = TranslateToLevelDTO(level);
                result.Add(newLevelDTO);
            }

            return result;
        }

        private LevelOutDTO TranslateToLevelDTO(Level level)
        {
            return new LevelOutDTO
            {
                LevelId = level.LevelId,
                LevelNumber = level.LevelNumber,
                Weight = level.Weight,
                Description = level.Description,
                Holders = TranslateToPersonDTOList(
                    _context.Users
                          .Include("UserLevels")
                          .Where(u => u.UserLevels
                                       .Select(l => l.LevelId)
                                                .Contains(level.LevelId))
                          .ToList()
                    )
            };
        }

        private List<PersonDTO> TranslateToPersonDTOList(List<User> users)
        {
            var result = new List<PersonDTO> { };
            foreach (User user in users)
            {
                var newUserDTO = TranslateToPersonDTO(user);
                result.Add(newUserDTO);
            }

            return result;
        }

        private PersonDTO TranslateToPersonDTO(User user)
        {
            return new PersonDTO
            {
                PersonId = user.UserId,
                Name = user.Name
            };
        }
    }
}
