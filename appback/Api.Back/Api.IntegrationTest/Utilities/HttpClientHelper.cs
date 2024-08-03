using Api.Common.Utils;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;

namespace Api.IntegrationTest.Utilities
{
    public class HttpClientHelper
    {
        private readonly HttpClient _httpClient;

        public HttpClientHelper(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _httpClient.GetAsync(url);
        }

        public async Task<ApiResponse<object>> PostAsync(string url, object data)
        {
            string json = JsonConvert.SerializeObject(data);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, body);
            string responseBody = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                ApiResponse<object> result = JsonConvert.DeserializeObject<ApiResponse<object>>(responseBody);
                return result;
            }
            else
            {
                ApiResponse<object> result = JsonConvert.DeserializeObject<ApiResponse<object>>(responseBody);
                return result;
            }
        }

        private bool TryDeserialize<T>(string json, out T result)
        {
            try
            {
                result = JsonConvert.DeserializeObject<T>(json);
                return true;
            }
            catch (JsonException)
            {
                result = default;
                return false;
            }
        }

        public async Task<bool> PutAsync(string url, object data)
        {
            string json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(url, content);
            string contentt = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) return true;
            return false;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await _httpClient.DeleteAsync(url);
        }
    }
}