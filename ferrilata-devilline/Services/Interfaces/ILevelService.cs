using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface ILevelService
    {
        Level FindById(long id);
        void AddLevel(long badgeId, LevelInDTO inputLevel);
        void UpdateLevel(long levelId, LevelInDTO inputLevel);
        List<Level> GetAll();
        void DeleteLevel(long id);
    }
}
