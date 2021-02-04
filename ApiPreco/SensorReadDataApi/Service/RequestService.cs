using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PrecoApi.Service
{
    public class RequestService<T>
    {
        private readonly HttpClient _httpClient;

        public RequestService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<T> SendResquest(string url, string header)
        {
            try
            {
                var httpClientHandler = new HttpClientHandler();

                httpClientHandler.ServerCertificateCustomValidationCallback = (messege, cert, chain, errors) => { return true; };

                using (var client = new HttpClient(httpClientHandler))
                {
                    T response = default;

                    if (!String.IsNullOrWhiteSpace(header))
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Add("x-api-key", header);
                    }

                    var responseRequest = await client.GetStringAsync(url);

                    if (responseRequest.Length > 0)
                        response = JsonSerializer.Deserialize<T>(responseRequest);

                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
