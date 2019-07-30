using System.Collections.Generic;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IBadgeService
    {
        List<BadgeDTO> GetAllDTO();
        List<Badge> GetAll();
    }
}