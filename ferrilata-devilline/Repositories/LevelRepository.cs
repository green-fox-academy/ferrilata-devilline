using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public void SaveOrUpdate(Level level)
        {
            if (_applicationContext.Levels.Find(level.LevelId) == null)
            {
                _applicationContext.Levels.Add(level);
            } else
            {
                _applicationContext.Update(level);
            }
            
            _applicationContext.SaveChanges();
        }

        public List<Level> RetrieveLevelsFromDB()
        {
            return _applicationContext.Levels.Include(level => level.UserLevels.Select(x => x.User)).ToList();
        }

        public Level FindLevelById(long id)
        {
            return _applicationContext.Levels.SingleOrDefault(x => x.LevelId == id);
        }

        public void DeleteLevelById(long id)
        {
            _applicationContext.Levels.Remove(FindLevelById(id));
            _applicationContext.SaveChanges();
        }
    }
}
