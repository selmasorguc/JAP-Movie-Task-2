namespace APITests
{
    using API.Controllers;
    using API.DTOs;
    using API.Interfaces;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestFixture]
    public class SearchMediaControllerNUnitTests
    {
        private Mock<IMovieRepository> _movieRepo;

        private MoviesController _moviesController;

        private List<MovieDto> movieDtos;

        [SetUp]
        public void SetUp()
        {
            _movieRepo = new Mock<IMovieRepository>();
            _moviesController = new MoviesController(_movieRepo.Object);
            movieDtos = new List<MovieDto>();

            MovieDto m1 = new MovieDto
            {
                Id = 1,
                Title = "Matrix",
                Description = "Movie about computers SCIFI",
                ReleaseDate = new DateTime(2021, 01, 01),
                CoverUrl = "www.img.com",
                IsMovie = true,
                Ratings = new List<RatingDto>
                {
                    new RatingDto{ Value = 5, MovieId = 1},
                    new RatingDto{ Value = 5, MovieId = 1}
                }

            };
            MovieDto m2 = new MovieDto
            {
                Id = 2,
                Title = "SING",
                Description = "Cartoon",
                ReleaseDate = new DateTime(2021, 01, 01),
                CoverUrl = "www.img.com",
                IsMovie = true,
                Ratings = new List<RatingDto>
                {
                    new RatingDto{ Value = 5, MovieId = 2},
                    new RatingDto{ Value = 5, MovieId = 2}
                }

            };

            movieDtos.Add(m1);
            movieDtos.Add(m2);
        }

        [Test]
        public async Task SearchMovieAsyncCheck_StringQueryInput_VerifyCalledMethod()
        {
            await _moviesController.SearchMoviesAsync("string");
            _movieRepo.Verify(x => x.SearchMediaAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
