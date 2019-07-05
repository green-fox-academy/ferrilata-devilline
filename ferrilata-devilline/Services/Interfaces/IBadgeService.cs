using ferrilata_devilline.Models;
using System.Collections.Generic;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IBadgeService
    {
        List<Badge> GetAll();
    }
}