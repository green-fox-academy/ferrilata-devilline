namespace ferrilata_devilline.Services.Interfaces
{
    public interface ISlackMessagingService
    {
        string BuildMessage();
        void SendMessage(string postData);
    }
}