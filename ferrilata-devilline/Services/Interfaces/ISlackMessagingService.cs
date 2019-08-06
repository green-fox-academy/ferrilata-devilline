namespace ferrilata_devilline.Services.Interfaces
{
    public interface ISlackMessagingService
    {
        string BuildMessage(string text, string user);
        void SendMessage(string postData);
    }
}