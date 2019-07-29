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

        public BadgeService(IBadgeRepository badgeRepository, IMapper mapper)
        {
            _badgeRepository = badgeRepository;
            _mapper = mapper;
        }

        public Badge FindById(long id)
        {
            return _badgeRepository.FindBadgeById(id);
        }
               
        public List<Badge> GetAll()
        {
            return _badgeRepository.RetrieveBadgesFromDB();
        }

        public List<BadgeDTO> GetAllDTO()
        {
            return _mapper.Map<List<Badge>, List<BadgeDTO>>(_badgeRepository.RetrieveBadgesFromDB());
        }

        public BadgeDTO FinDTOById(long id)
        {
            return GetAllDTO().SingleOrDefault(x => x.BadgeId == id);
        }

        //public void AddLevelToBadge(Badge badge, Level level)
        //{
        //    badge.Levels.Add(level);
        //    _badgeRepository.SaveOrUpdate(badge);
        //}
    }
}