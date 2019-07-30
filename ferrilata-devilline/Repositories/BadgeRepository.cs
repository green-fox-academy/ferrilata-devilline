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

        public void Save(Badge badge)
        {
            _applicationContext.Badges.Add(badge);
            _applicationContext.SaveChanges();
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
    }
}