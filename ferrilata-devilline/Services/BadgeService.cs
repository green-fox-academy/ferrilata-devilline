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

        public List<BadgeOutDTO> GetAll()
        {
            return _mapper.Map<List<BadgeOutDTO>>(_badgeRepository.RetrieveBadgesFromDB());
        }

        public Badge FindBadgeById(long id)
        {
            return _badgeRepository.FindBadgeById(id);
        }

        public BadgeOutDTO FindBadgeOutDTOById(long id)
        {
            return _mapper.Map<BadgeOutDTO>(FindBadgeById(id));
        }

        public List<LevelOutDTO> FindBadgeLevelsByBadgeId(long id)
        {
            return FindBadgeOutDTOById(id).Levels;
        }

        public void AddBadge(BadgeInDTO IncomingBadge)
        {
            Badge NewBadge = _mapper.Map<Badge>(IncomingBadge);
            _badgeRepository.SaveOrUpdateBadge(NewBadge);

            foreach (Level each in NewBadge.Levels)
            {
                each.Badge = NewBadge;
                _badgeRepository.SaveOrUpdateLevel(each);
            }
        }

        public void DeleteLevelById(long LevelId)
        {
            _badgeRepository.DeleteLevelById(LevelId);
        }
    }
}