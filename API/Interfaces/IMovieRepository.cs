namespace API.Interfaces
{
    using API.DTOs;
    using API.Entity;
    using API.Helpers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMovieRepository
    {
        Task<bool> SaveAllAsync();
        Task<List<MovieDto>> GetMoviesAsync();
        Task<MovieDto> GetMovieByIdAsync(int id);
        Task<List<MovieDto>> GetTVShowsAsync();
        Task<double> RateMovieAsync(Rating rating);
        Task<List<MovieDto>> SearchMediaAsync(string query);
        Task<List<MovieDto>> GetPaged(MovieParams movieParams);
        Task<List<MovieDto>> GetTVShowsPaged(MovieParams movieParams);
        Task<ServiceResponse<double>> GetAverageRatingAsync(int movieId);
    }
}
