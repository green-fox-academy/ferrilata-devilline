﻿using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using System.Collections.Generic;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface ILevelService
    {
        Level FindById(long id);
        void AddLevel(long badgeId, LevelInDTO inputLevel);
        void UpdateLevel(long levelId, LevelInDTO inputLevel);
        List<Level> GetAll();
        void DeleteLevel(long id);
        LevelOutDTO GetLevelOutDTO(long id);

    }
}
