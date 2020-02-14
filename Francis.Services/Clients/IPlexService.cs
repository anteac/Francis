using Francis.Models.Plex;
using Refit;
using System.Threading.Tasks;

namespace Francis.Services.Clients
{
    public interface IPlexService
    {
        [Get("/api/v2/pins/{pinId}.json")]
        Task<PlexPin> GetPin(string pinId, [Header("X-Plex-Client-Identifier")] string clientId);

        [Get("/users/account.json")]
        Task<PlexUserContainer> GetMe([Header("X-Plex-Token")] string token);
    }
}
