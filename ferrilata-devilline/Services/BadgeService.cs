using System.Collections;
using System.Collections.Generic;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Interfaces;

namespace ferrilata_devilline.Services
{
    public class BadgeService : IBadgeService
    {
        private readonly IBadgeRepository _badgeRepository;
        private readonly ILevelRepository _levelRepository;

        public BadgeService(IBadgeRepository badgeRepository, ILevelRepository levelRepository)
        {
            _badgeRepository = badgeRepository;
            _levelRepository = levelRepository;
        }

        public List<BadgeDTO> GetAll()
        {
            List<BadgeDTO> BadgeDTOList = new List<BadgeDTO>();
            List<Badge> BadgeList = _badgeRepository.RetrieveBadgesFromDB();

            return BadgeDTOList;
        }
    }
}