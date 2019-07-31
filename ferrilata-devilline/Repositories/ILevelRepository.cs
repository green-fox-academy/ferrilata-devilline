using ferrilata_devilline.Models.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Repositories
{
    public interface ILevelRepository
    {
        void SaveOrUpdate(Level level);
        List<Level> RetrieveLevelsFromDB();
        Level FindLevelById(long id);
        void DeleteLevelById(long id);
    }
}
