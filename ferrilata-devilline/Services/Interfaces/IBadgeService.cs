using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using System.Collections.Generic;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IBadgeService
    {
        List<BadgeOutDTO> GetAll();
        void AddBadge(BadgeInDTO IncomingBadge);
        BadgeOutDTO FindBadgeOutDTOById(long id);
        List<LevelOutDTO> FindBadgeLevelsByBadgeId(long id);
        void DeleteLevelById(long LevelId);
    }
}