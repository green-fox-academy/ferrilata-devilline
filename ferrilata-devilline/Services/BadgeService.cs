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
        public Badge FindBadge(long id)
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

        public BadgeDTO FindDTOById(long id)
        {
            return GetAllDTO().SingleOrDefault(x => x.BadgeId == id);
        }

        public void AddBadge(BadgeInDTO IncomingBadge)
        {
            Badge NewBadge = _mapper.Map<Badge>(IncomingBadge);
            _badgeRepository.SaveBadge(NewBadge);
        }

        public void DeleteById(long id)
        {
            _badgeRepository.DeleteBadgeById(id);
        }
    }
}