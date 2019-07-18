using ferrilata_devilline.Models.DTOs;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface ILevelService
    {
        void TranslateAndSaveLevelsFrom(BadgeDTO badgeDTO);
    }
}
