using System.Net;

namespace VintageCashCowTechTestUI.Client.Services
{
    public class HttpResponseMessageHandler : IHttpResponseMessageHandler
    {
        private void Log(HttpResponseMessage httpResponseMessage)
        {
            var requestUri = httpResponseMessage.RequestMessage?.RequestUri;
            var requestMethod = httpResponseMessage.RequestMessage?.Method;
            var requestId = "";
            if (httpResponseMessage.Headers.TryGetValues("x-vcc-productapi-requestid", out IEnumerable<string>? values))
            {
                requestId = values?.FirstOrDefault();
            }
            Console.WriteLine($"RequestUri: {requestUri}, RequestMethod: {requestMethod} RequestId: {requestId}");
        }

        public async Task<T?> Handle<T>(HttpResponseMessage? httpResponseMessage)
        {
            ArgumentNullException.ThrowIfNull(httpResponseMessage, nameof(httpResponseMessage));

            Log(httpResponseMessage);

            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ValidationException(content);
            }

            httpResponseMessage.EnsureSuccessStatusCode();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
        }
    }
}
