using ferrilata_devilline.Models.DAOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ferrilata_devilline.Repositories
{
    public class LevelRepository : ILevelRepository
    {
        private readonly ApplicationContext _applicationContext;

        public LevelRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void SaveOrUpdate(Level level)
        {
            if (GetById(level.LevelId) == null)
            {
                _applicationContext.Levels.Add(level);
            } else
            { 
            _applicationContext.Levels.Update(level);
            }
            _applicationContext.SaveChanges();
        }

        public Level GetById(long levelId)
        {
            return GetAllWithBadges().Where(x => x.LevelId == levelId).FirstOrDefault();
        }

        public List<Level> GetAllWithBadges()
        {
            return _applicationContext.Levels.Include(l => l.Badge).ToList();
        }
    }
}
