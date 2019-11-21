using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Francis.Toolbox.Helpers
{
    public static class WebResource
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async Task<bool> Exists(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return false;
            }

            using var request = new HttpRequestMessage(HttpMethod.Head, new Uri(imageUrl));
            using var response = await _client.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
    }
}
