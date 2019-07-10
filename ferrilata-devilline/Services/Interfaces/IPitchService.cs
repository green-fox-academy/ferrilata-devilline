using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DAOs;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IPitchService
    {
        Pitches GetPitches(string userEmail);
        void Save(Pitch pitch);
    }
}