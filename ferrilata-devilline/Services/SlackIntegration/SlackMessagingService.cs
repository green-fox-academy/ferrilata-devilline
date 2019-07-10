using System;
using System.Net.Http;
using System.Text;
using ferrilata_devilline.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace ferrilata_devilline.Services.SlackIntegration
{
    public class SlackMessagingService : ISlackMessagingService
    {
        private readonly IOptions<SlackOptions> _settings;

        public SlackMessagingService(IOptions<SlackOptions> settings)
        {
            _settings = settings;
        }

        public string BuildMessage(string text, string username)
        {
            string textMessage = $"{text} {username}";

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
            var response = await client.SendAsync(request);
            string responseStatus = response.StatusCode.ToString();
        }
    }
}