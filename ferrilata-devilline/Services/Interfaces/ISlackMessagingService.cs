namespace ferrilata_devilline.Services.Interfaces
{
    public interface ISlackMessagingService
    {
        string BuildMessage(string text, string username);
        void SendMessage(string postData);
    }
}