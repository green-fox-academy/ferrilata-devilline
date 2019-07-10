using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Io;

namespace ferrilata_devilline.IntegrationTests.Fixtures
{
    public class HtmlReader
    {
        public static async Task<IHtmlDocument> GetDocumentAsync(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            Action<VirtualResponse> action = x => x.Address(response.RequestMessage.RequestUri)
                                                    .Status(response.StatusCode)
                                                    .Headers(response.Headers)
                                                    .Headers(response.Content.Headers)
                                                    .Content(content);

            var document = await BrowsingContext.New()
                .OpenAsync(action, CancellationToken.None);
            return (IHtmlDocument)document;
        }
    }
}
