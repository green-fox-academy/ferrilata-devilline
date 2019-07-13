using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ferrilata_devilline.Models.DTOs;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IBadgeAndLevelService
    {
        List<BadgeOutDTO> GetAndTranslateToBadgeDTOAll();

        void TranslateAndSave(JToken requestBody);
    }
}