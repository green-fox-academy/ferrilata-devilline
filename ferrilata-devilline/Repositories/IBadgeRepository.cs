using System.Collections.Generic;
using ferrilata_devilline.Models.DAOs;

namespace ferrilata_devilline.Repositories
{
    public interface IBadgeRepository
    {
        void Save(Badge badge);
        void Update();
        bool CheckBadge(long id);
        List<Badge> RetrieveBadgesFromDB();
        Badge FindBadgeById(long id);
        void DeleteBadgeById(long id);
    }
}