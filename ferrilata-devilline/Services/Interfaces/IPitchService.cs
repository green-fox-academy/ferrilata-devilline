using ferrilata_devilline.Models.DAOs;
using ferrilata_devilline.Models.DTOs;
using Newtonsoft.Json.Linq;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IPitchService
    {
        Pitches GetPitches(string userEmail);

        void Save(Pitch pitch);

        void TranslateAndSave(JToken requestBody);

        void TranslateAndUpdate(JToken requestBody);
    }
}