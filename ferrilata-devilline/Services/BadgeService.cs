using System.Collections.Generic;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Interfaces;

namespace ferrilata_devilline.Services
{
    public class BadgeService : IBadgeService
    {
        private readonly IBadgeRepository _badgeRepository;

        public BadgeService(IBadgeRepository badgeRepository)
        {
            _badgeRepository = badgeRepository;
        }

        public Badge FindById(long id)
        {
            return _badgeRepository.FindBadgeById(id);
        }

        public List<Badge> GetAll()
        {
            return _badgeRepository.RetrieveBadgesFromDB();
        }
    }
}