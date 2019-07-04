using System.IO;

namespace ferrilata_devilline.Controllers
{
    internal class HttpRequestWrapper
    {
        private object httpRequest;

        public HttpRequestWrapper(object httpRequest)
        {
            this.httpRequest = httpRequest;
        }

        public Stream InputStream { get; internal set; }
    }
}