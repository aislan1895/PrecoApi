using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PrecoApi.Controller
{
    public class RequestServices<T>
    {
        private readonly HttpClient _httpClient;

        public RequestServices(HttpClient httpClient)
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
