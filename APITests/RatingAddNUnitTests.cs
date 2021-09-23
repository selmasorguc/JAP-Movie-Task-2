namespace APITests
{
    using API.Data;
    using API.Entity;
    using API.Helpers;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class RatingAddNUnitTests
    {
        private Rating rating1;

        private Movie movie1;

        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            movie1 = new Movie
            {
                Id = 1,
                IsMovie = true,
                Title = "SING",
                ReleaseDate = new DateTime(2021, 01, 01),
                CoverUrl = "https://media-cache.cinematerial.com/p/500x/fntb61gp/sing-movie-cover.jpg?v=1486583799",
                Description = "Cartoon about singing animals",
                Ratings = new List<Rating>()
            };

            rating1 = new Rating
            {
                Id = 1,
                MovieId = 1,
                Movie = movie1,
                Value = 5
            };

            var mappingConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new AutoMapperProfiles());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Test] 
        public async Task RatingAddChecker_Rating_One_CheckValuesFromDB()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "temp-movieapp").Options;
            var context = new DataContext(options);

            //act
            context.Movies.Add(movie1);
            context.SaveChanges();
            var movieRepository = new MovieRepository(context, _mapper);
            await movieRepository.RateMovieAsync(rating1);

            //assert
            var movieFromDB = context.Movies.FirstOrDefault(x => x.Title == "SING");
            var ratingFromDB = context.Ratings.FirstOrDefault(x => x.MovieId == movieFromDB.Id && x.Value == 5);
            Assert.Multiple(() =>
            {
                Assert.That(rating1.Value, Is.EqualTo(ratingFromDB.Value));
                Assert.That(rating1.MovieId, Is.EqualTo(ratingFromDB.MovieId));
            });
        }
    }
}
