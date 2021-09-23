using System.Threading.Tasks;
using API.Entity;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("ratings")]
    public class RatingsController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public RatingsController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        
        //return average rating of a movie with given movieId
        [HttpGet("average/{movieId}")]
        public async Task<ActionResult<ServiceResponse<double>>> GetAverageRating(int movieId)
        {
            var response = await _movieRepository.GetAverageRatingAsync(movieId);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        //add new rating to a movie and a new rating to the db
        [HttpPost("add")]
        public async Task<ActionResult<double>> AddRating(Rating rating)
        {
            return await _movieRepository.RateMovieAsync(rating);

        }
    }
}