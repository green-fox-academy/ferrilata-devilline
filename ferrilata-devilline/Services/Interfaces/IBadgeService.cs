using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using System.Collections.Generic;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IBadgeService
    {
        List<Badge> GetAll();
        bool BadgeExists(long badgeId);
        void TranslateAndUpdateBadgeFrom(BadgeDTO badgeDTO);
    }
}