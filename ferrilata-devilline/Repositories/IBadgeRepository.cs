using System.Collections.Generic;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;

namespace ferrilata_devilline.Repositories
{
    public interface IBadgeRepository
    {
        void Update();
        bool CheckBadge(long id);
        void SaveBadge(Badge badge);
        List<Badge> RetrieveBadgesFromDB();
        Badge FindBadgeById(long id);
        Level FindLevelById(long id);
        void DeleteBadgeById(long id);
    }
}