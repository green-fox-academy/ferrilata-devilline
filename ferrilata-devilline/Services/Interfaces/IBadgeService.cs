using System.Collections.Generic;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
<<<<<<< HEAD
using System.Collections.Generic;
=======
>>>>>>> dca29adbc1c64634539bf15144d7ad2c958baad5

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IBadgeService
    {
        Badge FindBadge(long id);
        List<BadgeDTO> GetAllDTO();
        List<Badge> GetAll();
        BadgeDTO FinDTOById(long id);
        void DeleteById(long id);
    }
}