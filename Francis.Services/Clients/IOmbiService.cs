using Francis.Models.Ombi;
using Refit;
using System.Threading.Tasks;

namespace Francis.Services.Clients
{
    public interface IOmbiService
    {
        [Get("/api/v1/Settings/about")]
        Task<AboutOmbi> About();

        [Get("/api/v1/Identity/Users")]
        Task<OmbiUser[]> GetUsers();

        [Get("/api/v1/Identity/User/{id}")]
        Task<OmbiUser> GetUser(string id);

        [Get("/api/v1/Request/movie")]
        Task<MovieRequest[]> GetMovieRequests();

        [Get("/api/v1/Request/tv")]
        Task<TvRequest[]> GetTvRequests();

        [Post("/api/v2/Search/multi/{search}")]
        [Headers("accept: text/plain", "Content-Type: application/json-patch+json")]
        Task<MultiSearchResult[]> SearchMulti(string search, [Body]string body = "{\"movies\":true,\"tvShows\":true,\"music\":false,\"people\":false}");

        [Get("/api/v2/Search/tv/{id}")]
        Task<TvSearchResult> GetTv(string id);

        [Get("/api/v2/Search/movie/{id}")]
        Task<MovieSearchResult> GetMovie(string id);

        [Post("/api/v1/Request/movie")]
        Task<RequestResult> RequestMovie([Body] object body);

        [Post("/api/v1/Request/movie/approve")]
        Task ApproveMovie([Body] object body);

        [Put("/api/v1/Request/movie/deny")]
        Task DenyMovie([Body] object body);

        [Post("/api/v2/Requests/tv")]
        Task<RequestResult> RequestTv([Body] object body);

        [Post("/api/v1/Request/tv/approve")]
        Task ApproveTv([Body] object body);

        [Put("/api/v1/Request/tv/deny")]
        Task DenyTv([Body] object body);
    }

    public interface IBotOmbiService : IOmbiService
    {
        [Get("/api/v1/Settings/about")]
        new Task<AboutOmbi> About();
    }
}
