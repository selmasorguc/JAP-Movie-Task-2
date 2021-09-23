using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MoviesController : ControllerBase
    {

        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieDto>>> GetMovies()
        {
            return Ok(await _movieRepository.GetMoviesAsync());
        }

        [HttpGet("/tvshows")]
        public async Task<ActionResult<List<MovieDto>>> GetTVShows()
        {
            return Ok(await _movieRepository.GetTVShowsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<MovieDto>>> GetMovieById(int id)
        {
            return Ok(await _movieRepository.GetMovieByIdAsync(id));
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<MovieDto>>> SearchMoviesAsync(
            [FromQuery] string query)
        {
            return Ok(await _movieRepository.SearchMediaAsync(query));
        }


        [HttpGet("paged")]
        public async Task<ActionResult<List<MovieDto>>> GetMoviesPaged(
            [FromQuery] MovieParams movieParams)
        {
            var movies = await _movieRepository.GetPaged(movieParams);
            return Ok(movies);
        }

        [HttpGet("tvshows/paged")]
        public async Task<ActionResult<List<MovieDto>>> GetTVShowsPaged(
            [FromQuery] MovieParams movieParams)
        {
            var tvshows = await _movieRepository.GetTVShowsPaged(movieParams);
            return Ok(tvshows);
        }
    }
}