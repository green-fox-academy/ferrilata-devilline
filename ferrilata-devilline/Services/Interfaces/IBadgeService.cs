﻿using ferrilata_devilline.Models.DAOs;
using System.Collections.Generic;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IBadgeService
    {
        List<Badge> GetAll();
    }
}