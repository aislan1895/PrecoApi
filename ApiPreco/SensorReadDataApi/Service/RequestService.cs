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
                T response = default;

                if (!String.IsNullOrWhiteSpace(header))
                {
                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Add("x-api-key", header);
                }

                var responseRequest = await _httpClient.GetStringAsync(url);

                if (responseRequest.Length > 0)
                    response = JsonSerializer.Deserialize<T>(responseRequest);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
