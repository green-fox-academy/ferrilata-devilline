using System.Collections.Generic;
using System.Linq;
using ferrilata_devilline.Models.DAOs;
using Microsoft.EntityFrameworkCore;

namespace ferrilata_devilline.Repositories
{
    public class BadgeRepository : IBadgeRepository
    {
        private readonly ApplicationContext _applicationContext;

        public BadgeRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void SaveOrUpdate(Badge badge)
        {
            if (_applicationContext.Badges.Find(badge.BadgeId) == null)
            {
                _applicationContext.Badges.Add(badge);
            }

            _applicationContext.SaveChanges();
        }

        public List<Badge> RetrieveBadgesFromDB()
        {
            return _applicationContext.Badges.Include(badge => badge.Levels)
                .ThenInclude(x => x.UserLevels).ThenInclude(x => x.User).ToList();
        }

        public Badge FindBadgeById(long id)
        {
            return RetrieveBadgesFromDB().Find(x => x.BadgeId == id);
        }

        public void DeleteBadgeById(long id)
        {
            _applicationContext.Badges.Remove(FindBadgeById(id));
            _applicationContext.SaveChanges();
        }

        public void SaveOrUpdateBadge(Badge badge)
        {
            if (_applicationContext.Badges.Find(badge.BadgeId) == null)
            {
                _applicationContext.Badges.Add(badge);
            }
            else
            {
                var oldBadge = FindBadgeById(badge.BadgeId);
                oldBadge = badge;
            }
            _applicationContext.SaveChanges();
        }

        public void SaveOrUpdateLevel(Level level)
        {
            if (_applicationContext.Levels.Find(level.LevelId) == null)
            {
                _applicationContext.Levels.Add(level);
            }
            else
            {
                var oldLevel = FindLevelById(level.LevelId);
                oldLevel = level;
            }

            _applicationContext.SaveChanges();
        }

        public Level FindLevelById(long levelId)
        {
            return RetrieveLevelsFromDB().Find(x => x.LevelId == levelId);
        }

        public List<Level> RetrieveLevelsFromDB()
        {
            return _applicationContext.Levels.Include(level => level.Badge).ToList();
        }


    }
}