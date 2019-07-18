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
    }
}
