﻿using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using System.Collections.Generic;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IBadgeService
    {
        List<BadgeDTO> GetAndTranslateAll();
    }
}