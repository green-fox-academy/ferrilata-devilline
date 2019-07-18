using System.Collections.Generic;
using System.Linq;
using ferrilata_devilline.Models.DAOs;
using Microsoft.EntityFrameworkCore;

namespace ferrilata_devilline.Repositories
{
    public class LevelRepository : ILevelRepository
    {
        private readonly ApplicationContext _applicationContext;

        public LevelRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public List<Level> RetrieveLevelsFromDB()
        {
            return _applicationContext.Levels.Include(level => level.UserLevels.Select(x => x.User)).ToList();
        }
    }
}