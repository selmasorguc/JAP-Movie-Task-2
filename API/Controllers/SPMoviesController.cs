using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entity;
using API.Entity.StoredProceduresEntites;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("spmovies")]
    public class SPMoviesController : ControllerBase
    {
        private readonly IMoviesSPService _moviesSPService;
        public SPMoviesController(IMoviesSPService moviesSPService)
        {
            _moviesSPService = moviesSPService;
        }

        [HttpGet]
        public async Task<ServiceResponse<List<Top10Item>>> Get10TopRatedMovies()
        {
            var response = new ServiceResponse<List<Top10Item>>();
            response = await _moviesSPService.GetTop10MoviesAsync();
            return response;
        }
    }
}