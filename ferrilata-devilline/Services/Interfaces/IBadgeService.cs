using ferrilata_devilline.Models.DTOs;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IBadgeAndLevelService
    {
        List<BadgeOutDTO> GetAndTranslateToBadgeDTOAll();

        void TranslateAndSave(JToken requestBody);
    }
}