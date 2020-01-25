using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Francis.Toolbox.Helpers
{
    public static class WebResource
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async Task<bool> Exists(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }

            using var request = new HttpRequestMessage(HttpMethod.Head, new Uri(url));
            using var response = await _client.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
    }
}
