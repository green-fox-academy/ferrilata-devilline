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

        public void SaveOrUpdate(Badge badge)
        {
            if (FindBadgeById(badge.BadgeId) != null)
            {
                _applicationContext.Badges.Add(badge);
            }
            else
            {
                _applicationContext.Badges.Update(badge);
            }

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