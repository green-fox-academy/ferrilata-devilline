using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using System.Collections.Generic;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IBadgeService
    {
        Badge FindBadge(long id);
        BadgeDTO FinDTOById(long id);
        List<BadgeDTO> GetAllDTO();
        List<Badge> GetAll();
        void DeleteById(long id);
    }
}