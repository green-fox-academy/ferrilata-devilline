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

        public void DeleteLevel(long id)
        {
            _levelRepository.DeleteLevelById(id);
        }

        public void AddLevel(long badgeId, LevelInDTO inputLevel)
        {
            Level newLevel = _mapper.Map<LevelInDTO, Level>(inputLevel);
            newLevel.Badge = _badgeRepository.FindBadgeById(badgeId);
            _levelRepository.SaveOrUpdate(newLevel);
        }

        public void UpdateLevel(long levelId, LevelInDTO inputLevel)
        {
            var levelToUpdate = FindById(levelId);

            levelToUpdate.Weight = inputLevel.Weight ?? levelToUpdate.Weight;
            levelToUpdate.Description = inputLevel.Description ?? levelToUpdate.Description;
            levelToUpdate.LevelNumber = inputLevel.LevelNumber != 0
                ? inputLevel.LevelNumber
                : levelToUpdate.LevelNumber;
            _levelRepository.SaveOrUpdate(levelToUpdate);
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