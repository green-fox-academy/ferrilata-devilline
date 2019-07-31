using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using System.Collections.Generic;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface ILevelService
    {
        Level FindById(long id);
        void AddLevel(long badgeId, LevelInDTO inputLevel);
        List<Level> GetAll();
        LevelOutDTO GetLevelOutDTO(long id);
    }
}
