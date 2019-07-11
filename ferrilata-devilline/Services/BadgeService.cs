using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Translators;

namespace ferrilata_devilline.Services
{
    public class BadgeService : IBadgeService
    {
        readonly ApplicationContext _context;

        public BadgeService(ApplicationContext context)
        {
            _context = context;
        }

        public List<BadgeDTO> GetAndTranslateToBadgeDTOAll()
        {
            var Translator = new BadgeOutDTOTranslator(_context);
            var badges = _context.Badges.ToList();

            var resultBadges = Translator.Translate(badges);

            return resultBadges;
        }

      
    }
}