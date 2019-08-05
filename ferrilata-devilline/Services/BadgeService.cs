using System.Collections.Generic;
using System.Linq;
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
        private readonly ILevelRepository _levelRepository;

        public BadgeService(IBadgeRepository badgeRepository, IMapper mapper, ILevelRepository levelRepository)
        {
            _badgeRepository = badgeRepository;
            _mapper = mapper;
            _levelRepository = levelRepository;
        }

        public Badge FindBadge(long id)
        {
            return _badgeRepository.FindBadgeById(id);
        }

        public List<BadgeDTO> GetAllDTO()
        {
            var badgeDTOList = _mapper.Map<List<Badge>, List<BadgeDTO>>(_badgeRepository.RetrieveBadgesFromDB());
            return badgeDTOList;
        }

        public List<Badge> GetAll()
        {
            return _badgeRepository.RetrieveBadgesFromDB();
        }


        public void AddBadge(BadgeInDTO IncomingBadge)
        {
            Badge NewBadge = _mapper.Map<Badge>(IncomingBadge);
            _badgeRepository.SaveBadge(NewBadge);
        }

        public BadgeDTO FindDTOById(long id)
        {
            return GetAllDTO().SingleOrDefault(x => x.BadgeId == id);
        }

        public List<LevelOutDTO> FinLevelsDTOByBadgeId(long id)
        {
            return FindDTOById(id).Levels;
        }

        public void DeleteById(long id)
        {
            _badgeRepository.DeleteBadgeById(id);
        }

        public void UpdateBadge(long badgeId, BadgeInDTO inputBadge)
        {
            var badgeToUpdate = FindBadge(badgeId);

            badgeToUpdate.Name = inputBadge.Name ?? badgeToUpdate.Name;
            badgeToUpdate.Version = inputBadge.Version > 0 ? inputBadge.Version : badgeToUpdate.Version;
            badgeToUpdate.Tag = inputBadge.Tag ?? badgeToUpdate.Name;
            _badgeRepository.Update();
        }

        public void UpdateBadgeLevels(long badgeId, BadgeInDTO inputBadge)
        {
            var badgeToUpdate = FindBadge(badgeId);
            var badgeLevels = badgeToUpdate.Levels;
            foreach (var level in badgeLevels)
            {
                _levelRepository.DeleteLevelById(level.LevelId);
            }

            var newLevels = _mapper.Map<List<LevelInDTO>, List<Level>>(inputBadge.Levels);
            foreach (var level in newLevels)
            {
                level.Badge = badgeToUpdate;
                _levelRepository.SaveOrUpdate(level);
            }

            _badgeRepository.Update();
        }
    }
}