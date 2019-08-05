using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using System.Collections.Generic;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IBadgeService
    {
        Badge FindBadge(long id);
        BadgeDTO FindDTOById(long id);
        List<BadgeDTO> GetAllDTO();
        List<Badge> GetAll();
        void AddBadge(BadgeInDTO IncomingBadge);
        void DeleteById(long id);
        List<LevelOutDTO> FinLevelsDTOByBadgeId(long id);
        void UpdateBadge(long badgeId, BadgeInDTO inputBadge);
        void UpdateBadgeLevels(long badgeId, BadgeInDTO inputBadge);
    }
}