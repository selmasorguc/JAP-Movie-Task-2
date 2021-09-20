using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.DTOs;
using API.Entity;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public MovieRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MovieDto>> GetMoviesAsync()
        {
            var movies = await _context.Movies
            .Include(m => m.Cast).AsSplitQuery()
            .Include(m => m.Ratings)
            .Include(m => m.Screenings)
            .Where(m => m.IsMovie == true)
            .OrderByDescending(x => x.Ratings.Select(x => x.Value).Average())
            .ToListAsync();
            var moviesDto = _mapper.Map<IEnumerable<MovieDto>>(movies);
            return moviesDto;

        }

        public async Task<IEnumerable<MovieDto>> GetPaged(MovieParams movieParams)
        {

            var movies = await _context.Movies
            .Include(m => m.Cast).AsSplitQuery()
            .Include(m => m.Ratings)
            .Include(m => m.Screenings)
            .Where(m => m.IsMovie == true)
            .OrderByDescending(x => x.Ratings.Select(x => x.Value).Average())
            .Skip((movieParams.PageNumber - 1) * movieParams.PageSize)
            .Take(movieParams.PageSize)
            .ToListAsync();
            var moviesDto = _mapper.Map<IEnumerable<MovieDto>>(movies);
            return moviesDto;


        }

        public async Task<IEnumerable<MovieDto>> GetTVShowsPaged(MovieParams movieParams)
        {
            var movies = await _context.Movies
            .Include(m => m.Cast).AsSplitQuery()
            .Include(m => m.Ratings)
            .Include(m => m.Screenings)
            .Where(m => m.IsMovie == false)
            .OrderByDescending(x => x.Ratings.Select(x => x.Value).Average())
            .Skip((movieParams.PageNumber - 1) * movieParams.PageSize)
            .Take(movieParams.PageSize)
            .ToListAsync();
            var moviesDto = _mapper.Map<IEnumerable<MovieDto>>(movies);
            return moviesDto;
        }


        public async Task<MovieDto> GetMovieByIdAsync(int id)
        {
            var movie = await _context.Movies
            .Include(m => m.Cast)
            .Include(m => m.Ratings)
            .Include(m => m.Screenings)
            .Where(m => m.IsMovie == true)
            .SingleOrDefaultAsync(m => m.Id == id);
            var movieDto = _mapper.Map<MovieDto>(movie);
            return movieDto;
        }
        public async Task<IEnumerable<MovieDto>> GetTVShowsAsync()
        {
            var tvshows = await _context.Movies.Include(m => m.Cast).Include(m => m.Screenings)
            .Include(m => m.Ratings).AsSplitQuery()
            .Where(m => m.IsMovie == false)
            .OrderByDescending(x => x.Ratings.Select(x => x.Value).Average())
            .ToListAsync();
            var tvshowsDto = _mapper.Map<IEnumerable<MovieDto>>(tvshows).ToList();

            return tvshowsDto;
        }

        public async Task<double> RateMovieAsync(Rating rating)
        {
            var movie = await _context.Movies
            .Include(m => m.Ratings).FirstOrDefaultAsync(m => m.Id == rating.MovieId);
            if (movie == null) return 0;
            _context.Ratings.Add(rating);
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return movie.Ratings.Select(x => x.Value).Average();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<MovieDto>> SearchMediaAsync(string query)
        {
            var movies = await _context.Movies
            .Include(m => m.Cast).AsSplitQuery()
            .Include(m => m.Ratings)
            .Where(m => m.Title.ToLower().Contains(query.ToLower()) ||
            m.Description.ToLower().Contains(query.ToLower()))
            .OrderByDescending(x => x.Ratings.Select(x => x.Value).Average())
            .ToListAsync();

            //Keywords check
            int numericValue;
            bool isNumber = int.TryParse(Regex.Match(query, @"\d+").Value, out numericValue);

            if (query.ToLower().Contains("star") && isNumber && numericValue.ToString().Length == 1)
            {
                if (query.ToLower().Contains("at least"))
                {
                    movies.AddRange(await _context.Movies
                        .Include(m => m.Cast).AsSplitQuery()
                        .Include(m => m.Ratings)
                        .Where(m => m.Ratings.Average(x => x.Value) >= numericValue)
                        .OrderByDescending(x => x.Ratings.Select(x => x.Value).Average())
                        .ToListAsync());
                }
                else
                {
                    movies.AddRange(await _context.Movies
                    .Include(m => m.Cast).AsSplitQuery()
                    .Include(m => m.Ratings)
                    .Where(m => m.Ratings.Average(x => x.Value) == numericValue)
                    .OrderByDescending(x => x.Ratings.Select(x => x.Value).Average())
                    .ToListAsync());
                }
            }

            if (query.ToLower().Contains("year") && isNumber && numericValue.ToString().Length == 1)
            {
                if (query.ToLower().Contains("older"))
                {
                    movies.AddRange(await _context.Movies
                        .Include(m => m.Cast).AsSplitQuery()
                        .Include(m => m.Ratings)
                        .Where(m => DateTime.Now.Year - m.ReleaseDate.Year >= numericValue)
                        .OrderByDescending(x => x.Ratings.Select(x => x.Value).Average())
                        .ToListAsync());
                }
                else
                {
                    movies.AddRange(await _context.Movies
                        .Include(m => m.Cast).AsSplitQuery()
                        .Include(m => m.Ratings)
                        .Where(m => DateTime.Now.Year - m.ReleaseDate.Year <= numericValue)
                        .OrderByDescending(x => x.Ratings.Select(x => x.Value).Average())
                        .ToListAsync());
                }
            }

            if (isNumber && numericValue.ToString().Length == 4)
            {
                if (query.ToLower().Contains("after"))
                {
                    movies.AddRange(await _context.Movies
                        .Include(m => m.Cast).AsSplitQuery()
                        .Include(m => m.Ratings)
                        .Where(m => m.ReleaseDate.Year > numericValue)
                        .OrderByDescending(x => x.Ratings.Select(x => x.Value).Average())
                        .ToListAsync());
                }
                else
                {
                   movies.AddRange(await _context.Movies
                        .Include(m => m.Cast).AsSplitQuery()
                        .Include(m => m.Ratings)
                        .Where(m => m.ReleaseDate.Year == numericValue)
                        .OrderByDescending(x => x.Ratings.Select(x => x.Value).Average())
                        .ToListAsync());
                }
            }


            var moviesDto = _mapper.Map<IEnumerable<MovieDto>>(movies).ToList();
            return moviesDto;
        }


    }
}