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

        [HttpPost("add")]
        public async Task<ActionResult<double>> AddRating(Rating rating)
        {
            return await _movieRepository.RateMovieAsync(rating);
            
        }



    }
}