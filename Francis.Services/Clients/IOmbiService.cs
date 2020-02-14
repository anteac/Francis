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

        [Get("/api/v1/Search/movie/{search}")]
        Task<MovieSearchResult[]> SearchMovie(string search);

        [Get("/api/v1/Search/tv/{search}")]
        Task<TvSearchResult[]> SearchTv(string search);

        [Get("/api/v1/Search/movie/info/{id}")]
        Task<MovieSearchResult> GetMovie(long id);

        [Get("/api/v1/Search/tv/info/{id}")]
        Task<TvSearchResult> GetTv(long id);

        [Post("/api/v1/Request/movie")]
        Task<RequestResult> RequestMovie([Body] object body);

        [Post("/api/v1/Request/movie/approve")]
        Task ApproveMovie([Body] object body);

        [Put("/api/v1/Request/movie/deny")]
        Task DenyMovie([Body] object body);

        [Post("/api/v1/Request/tv")]
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
