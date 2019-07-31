using AutoMapper;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Repositories;
using ferrilata_devilline.Services.Interfaces;
using System.Collections.Generic;

namespace ferrilata_devilline.Services
{
    public class LevelService : ILevelService
    {
        private readonly ILevelRepository _levelRepository;
        private readonly IBadgeRepository _badgeRepository;
        private readonly IMapper _mapper;

        public LevelService(ILevelRepository levelRepository, IMapper mapper, IBadgeRepository badgeRepository)
        {
            _levelRepository = levelRepository;
            _mapper = mapper;
            _badgeRepository = badgeRepository;
        }

        public List<Level> GetAll()
        {
            return _levelRepository.RetrieveLevelsFromDB();
        }

        public void AddLevel(long badgeId, LevelInDTO inputLevel)
        {
            Level newLevel = _mapper.Map<LevelInDTO, Level>(inputLevel);
            newLevel.Badge = _badgeRepository.FindBadgeById(badgeId);
            _levelRepository.SaveOrUpdate(newLevel);
        }

        public Level FindById(long id)
        {
            return _levelRepository.FindLevelById(id);
        }

        public LevelOutDTO GetLevelOutDTO(long id)
        {
            return _mapper.Map<Level, LevelOutDTO>(_levelRepository.FindLevelById(id));
        }
    }
}
