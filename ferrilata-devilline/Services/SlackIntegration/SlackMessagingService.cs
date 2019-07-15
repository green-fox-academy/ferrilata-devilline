using ferrilata_devilline.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text;

namespace ferrilata_devilline.Services.SlackIntegration
{
    public class SlackMessagingService : ISlackMessagingService
    {
        private readonly IOptions<SlackOptions> _settings;
        private readonly string _tempText;
        private readonly string _tempUser;

        public SlackMessagingService(IOptions<SlackOptions> settings)
        {
            _settings = settings;
            _tempText = "Placeholder text sent from";
            _tempUser = "placeholder user";
        }

        public string BuildMessage()
        {
            string textMessage = $"{_tempText} {_tempUser}";

            StringBuilder postData = new StringBuilder();
            postData.Append("payload={");

            if (!String.IsNullOrEmpty(textMessage))
            {
                postData.Append("\"text\":\"" + textMessage + "\"");
            }

            if (!String.IsNullOrEmpty(_settings.Value.ChannelName))
            {
                postData.Append(",\"channel\":\"" + _settings.Value.ChannelName + "\"");
            }

            postData.Append("}");
            return postData.ToString();
        }

        public async void SendMessage(string postData)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _settings.Value.WebhookUrl);
            request.Content = new StringContent(postData,
                Encoding.UTF8,
                "application/x-www-form-urlencoded");
            await client.SendAsync(request);
        }
    }
}