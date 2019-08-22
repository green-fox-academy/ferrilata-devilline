using ferrilata_devilline.Models.DAOs;
using System.Collections.Generic;

namespace ferrilata_devilline.Repositories
{
    public interface IPitchRepository
    {
        void SavePitch(Pitch pitch);
        List<Pitch> RetrievePitchesFromDB();
        Pitch FindPitchById(long id);
        void Update();
        void Delete(Pitch pitch);
    }
}
