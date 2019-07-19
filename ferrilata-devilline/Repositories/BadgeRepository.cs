using System.Collections.Generic;
using System.Linq;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
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

        public async void SaveOrUpdate(Badge badge)
        {
            //var badgeInDatabase = await _applicationContext.FindAsync(typeof(Badge), badge.BadgeId); //To avoid a tracking error
            var badgeInDatabase = FindBadgeById(badge.BadgeId);
            _applicationContext.Entry(badgeInDatabase).State = EntityState.Detached;

            if (badgeInDatabase == null)
            {
                _applicationContext.Badges.Add(badge);
            }

            var removedLevels = badgeInDatabase.Levels.Except(badge.Levels).ToList();
            _applicationContext.Levels.RemoveRange(removedLevels); // this is actually necessary for updating
            _applicationContext.Badges.Update(badge);

            _applicationContext.SaveChanges();
        }

        public List<Badge> RetrieveBadgesFromDB()
        {
            return _applicationContext.Badges.ToList();
        }

        public Badge FindBadgeById(long id)
        {
            return RetrieveBadgesFromDB().Where(x => x.BadgeId == id).FirstOrDefault();
        }

        public void DeleteBadgeById(long id)
        {
            _applicationContext.Badges.Remove(FindBadgeById(id));
            _applicationContext.SaveChanges();
        }
    }
}