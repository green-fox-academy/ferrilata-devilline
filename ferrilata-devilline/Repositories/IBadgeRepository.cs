using System.Collections.Generic;
using ferrilata_devilline.Models.DAOs;

namespace ferrilata_devilline.Repositories
{
    public interface IBadgeRepository
    {
        void SaveBadge(Badge badge);
        List<Badge> RetrieveBadgesFromDB();
        Badge FindBadgeById(long id);
        Level FindLevelById(long id);
        void DeleteBadgeById(long id);
    }
}