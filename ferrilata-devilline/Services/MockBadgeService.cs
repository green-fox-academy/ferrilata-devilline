﻿using System.Collections.Generic;
using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Services.Interfaces;

namespace ferrilata_devilline.Services
{
    public class MockBadgeService : IBadgeService
    {
        public List<Badge> GetAll()
        {
            return new List<Badge> { };
        }
    }
}
