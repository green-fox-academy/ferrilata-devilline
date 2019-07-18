using System.Collections.Generic;
using ferrilata_devilline.Models.DAOs;

namespace ferrilata_devilline.Repositories
{
    public interface ILevelRepository
    {
        List<Level> RetrieveLevelsFromDB();
    }
}