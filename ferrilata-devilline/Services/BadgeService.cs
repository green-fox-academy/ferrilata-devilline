using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Translators;
using Newtonsoft.Json.Linq;

namespace ferrilata_devilline.Services
{
    public class BadgeAndLevelService : IBadgeAndLevelService
    {
        readonly ApplicationContext _context;

        public BadgeAndLevelService(ApplicationContext context)
        {
            _context = context;
        }

        public void TranslateAndSave(JToken requestBody)
        {
            var inTranslator = new BadgeInDTOTranslator();
            var badge = inTranslator.TranslateBadge(requestBody);
            Save(badge);

            var levels = inTranslator.ExtractLevels(requestBody, badge);
            Save(levels);
        }

        public void Save(Badge badge)
        {
            _context.Badges.Add(badge);
            _context.SaveChanges();
        }

        public void Save(List<Level> levels)
        {
            _context.Levels.AddRange(levels);
            _context.SaveChanges();
        }

        public List<BadgeOutDTO> GetAndTranslateToBadgeDTOAll()
        {
            var outTranslator = new BadgeOutDTOTranslator(_context);
            var badges = _context.Badges.ToList();

            var resultBadges = outTranslator.Translate(badges);

            return resultBadges;
        }
    }
}