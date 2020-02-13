using Francis.Models.Ombi;
using Refit;
using System.Threading.Tasks;

namespace Francis.Services.Clients
{
    public interface IOmbiService
    {
        [Get("/api/v1/Settings/about")]
        public Task<AboutOmbi> About();

        [Get("/api/v1/Identity/Users")]
        public Task<OmbiUser[]> GetUsers();

        [Get("/api/v1/Identity/User/{id}")]
        public Task<OmbiUser> GetUser(string id);

        [Get("/api/v1/Request/movie")]
        public Task<MovieRequest[]> GetMovieRequests();

        [Get("/api/v1/Request/tv")]
        public Task<TvRequest[]> GetTvRequests();

        [Get("/api/v1/Search/movie/{search}")]
        public Task<MovieSearchResult[]> SearchMovie(string search);

        [Get("/api/v1/Search/tv/{search}")]
        public Task<TvSearchResult[]> SearchTv(string search);

        [Get("/api/v1/Search/movie/info/{id}")]
        public Task<MovieSearchResult> GetMovie(long id);

        [Get("/api/v1/Search/tv/info/{id}")]
        public Task<TvSearchResult> GetTv(long id);

        [Post("/api/v1/Request/movie")]
        public Task<RequestResult> RequestMovie([Body] object body);

        [Post("/api/v1/Request/movie/approve")]
        public Task ApproveMovie([Body] object body);

        [Put("/api/v1/Request/movie/deny")]
        public Task DenyMovie([Body] object body);

        [Post("/api/v1/Request/tv")]
        public Task<RequestResult> RequestTv([Body] object body);

        [Post("/api/v1/Request/tv/approve")]
        public Task ApproveTv([Body] object body);

        [Put("/api/v1/Request/tv/deny")]
        public Task DenyTv([Body] object body);
    }

    public interface IBotOmbiService : IOmbiService
    {
        [Get("/api/v1/Settings/about")]
        public new Task<AboutOmbi> About();
    }
}
