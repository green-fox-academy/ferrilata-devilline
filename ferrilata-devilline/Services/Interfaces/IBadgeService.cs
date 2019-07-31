using System.Collections.Generic;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IBadgeService
    {
        Badge FindBadge(long id);
        List<BadgeDTO> GetAllDTO();
        List<Badge> GetAll();
        void AddBadge(BadgeInDTO IncomingBadge);
        BadgeDTO FinDTOById(long id);
        void DeleteById(long id);
        List<LevelOutDTO> FinLevelsDTOByBadgeId(long id);
    }
}