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
        private readonly IMapper _mapper;

        public BadgeService(IBadgeRepository badgeRepository, IMapper mapper)
        {
            _badgeRepository = badgeRepository;
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
            _badgeRepository.SaveOrUpdate(badge);
        }
    }
}