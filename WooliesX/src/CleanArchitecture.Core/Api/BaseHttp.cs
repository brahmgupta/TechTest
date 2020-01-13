using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Api
{
    public abstract class BaseHttp
    {
        private readonly HttpClient _client;

        public BaseHttp(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> Get(string uri, CancellationToken cancellationToken)
        {
            var httpResponse = await _client.GetAsync(new Uri(uri));

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Error retrieving");
            }

            if (httpResponse?.Content == null)
            {
                throw new Exception("Null httpResponse");
            }

            string content;
            try
            {
                content = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex} Failed to read");
            }

            return content;
        }
    }
}
