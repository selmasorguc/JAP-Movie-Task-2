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
    using System.Threading.Tasks;

    [TestFixture]
    public class AverageRatingNUnitTests
    {
        private Rating rating1;

        private Rating rating2;

        DataContext context;

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
            };

            rating2 = new Rating
            {
                Id = 2,
                MovieId = 1,
                Movie = movie1,
            };

            var mappingConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new AutoMapperProfiles());
            });
            _mapper = mappingConfig.CreateMapper();

            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "temp-movieapp").Options;
            context = new DataContext(options);
        }

        [Test]
        [TestCase(5, 5, ExpectedResult = 5)]
        [TestCase(1, 1, ExpectedResult = 1)]
        [TestCase(4, 5, ExpectedResult = 4.5)]
        [TestCase(0, 0, ExpectedResult = 0)]

        public async Task<double> GetAverageRatingChecker_MovieIdInput_ReturnServiceResponseTypeDouble(double ratingValue1, double ratingValue2)
        {
            //arrange 
            context.Database.EnsureDeleted();

            context.Movies.Add(movie1);
            rating1.Value = ratingValue1;
            context.Ratings.Add(rating1);

            rating2.Value = ratingValue2;
            context.Ratings.Add(rating2);

            context.SaveChanges();
            var movieRepository = new MovieRepository(context, _mapper);

            //act
            var avgRating = await movieRepository.GetAverageRatingAsync(movie1.Id);

            //assert
            return avgRating.Data;
        }
    }
}
