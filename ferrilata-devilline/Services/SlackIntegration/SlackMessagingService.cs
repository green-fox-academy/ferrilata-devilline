using ferrilata_devilline.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace ferrilata_devilline.Services.SlackIntegration
{
    public class SlackMessagingService: ISlackMessagingService
    {
        public SlackMessagingService(IOptions<SlackOptions> settings)
        {
        }
    }
}