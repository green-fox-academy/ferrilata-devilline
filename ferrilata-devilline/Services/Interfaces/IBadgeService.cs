using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using System.Collections.Generic;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IBadgeService
    {
        List<Badge> GetAll();
        Badge FindById(long id);
        List<BadgeDTO> GetAllDTO();
        BadgeDTO FinDTOById(long id);
    }
}