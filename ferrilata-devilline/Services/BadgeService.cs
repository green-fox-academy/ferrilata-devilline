using System.Collections.Generic;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public BadgeService(IBadgeRepository badgeRepository, ILevelRepository levelRepository, IMapper mapper)
        {
            _badgeRepository = badgeRepository;
            _levelRepository = levelRepository;
            _mapper = mapper;
        }

        public List<Badge> GetAll()
        {
            return _badgeRepository.RetrieveBadgesFromDB();
        }

        public bool BadgeExists(long badgeId)
        {
            return _badgeRepository.FindBadgeById(badgeId) != null;
        }

        public void TranslateAndUpdateBadgeFrom(BadgeDTO badgeDTO)
        {
            var badge = _mapper.Map<BadgeDTO, Badge>(badgeDTO);
            var levels = new List<Level> { };
            foreach (var levelDTO in badgeDTO.Levels)
            {
                var level = _levelRepository.GetById(levelDTO.LevelId);
                levels.Add(level);
            }
            badge.Levels = levels;
            _badgeRepository.SaveOrUpdate(badge);
        }
    }
}