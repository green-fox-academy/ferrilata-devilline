using ferrilata_devilline.Models;

namespace ferrilata_devilline.Services.Interfaces
{
    public interface IPitchService
    {
        Pitches GetPitches();
        void SendMessageToSlack(string text, string username);
    }
}