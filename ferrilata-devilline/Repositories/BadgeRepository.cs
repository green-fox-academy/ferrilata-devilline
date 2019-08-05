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

        public void Update()
        {
            _applicationContext.SaveChanges();
        }

        public bool CheckBadge(long id)
        {
            return _applicationContext.Badges.Contains(FindBadgeById(id));
        }

        public List<Badge> RetrieveBadgesFromDB()
        {
            return _applicationContext.Badges.Include(badge => badge.Levels).ThenInclude(Level => Level.UserLevels)
                .ThenInclude(UserLevel => UserLevel.User).ToList();
        }

        public Badge FindBadgeById(long id)
        {
            return RetrieveBadgesFromDB().Find(x => x.BadgeId == id);
        }

        public void DeleteBadgeById(long id)
        {
            Badge badgeToDelete = FindBadgeById(id);
            _applicationContext.Badges.Remove(badgeToDelete);
            _applicationContext.SaveChanges();
        }

        public void SaveBadge(Badge badge)
        {
            _applicationContext.Badges.Add(badge);
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