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
            _applicationContext.Update(badge);
            _applicationContext.SaveChanges();
        }

        public List<Badge> RetrieveBadgesFromDB()
        {
            return _applicationContext.Badges.Include(badge => badge.Levels).ThenInclude(Level => Level.UserLevels).ThenInclude(UserLevel => UserLevel.User).ToList();
        }

        public Badge FindBadgeById(long id)
        {
            return RetrieveBadgesFromDB().SingleOrDefault(x => x.BadgeId == id);
        }

        public void DeleteBadgeById(long id)
        {
            _applicationContext.Badges.Remove(FindBadgeById(id));
            _applicationContext.SaveChanges();
        }

        
    }
}