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
    public class SearchMediaNUnitTests
    {
        private IMapper _mapper;

        private DataContext context;

        [SetUp]
        public void SetUp()
        {
            //Setting up mapper and context for movie repository 
            var mappingConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new AutoMapperProfiles());
            });
            _mapper = mappingConfig.CreateMapper();
            var options = new DbContextOptionsBuilder<DataContext>()
                          .UseInMemoryDatabase(databaseName: "temp-movieapp").Options;
            context = new DataContext(options);
            context.Database.EnsureDeleted();

            //Creating test data for in memory DB
            Rating rating1 = new Rating
            {
                Id = 1,
                MovieId = 1,
                Value = 5
            };

            Rating rating2 = new Rating
            {
                Id = 2,
                MovieId = 1,
                Value = 5
            };

            Rating rating3 = new Rating
            {
                Id = 3,
                MovieId = 2,
                Value = 4
            };

            Rating rating4 = new Rating
            {
                Id = 4,
                MovieId = 2,
                Value = 4
            };

            List<Rating> m1Ratings = new List<Rating>();
            m1Ratings.Add(rating1);
            m1Ratings.Add(rating2);

            List<Rating> m2Ratings = new List<Rating>();
            m2Ratings.Add(rating3);
            m2Ratings.Add(rating4);

            Actor a1 = new Actor
            {
                Id = 1,
                Name = "Brad Pitt"
            };

            Actor a2 = new Actor
            {
                Id = 2,
                Name = "Julia Roberts"
            };

            List<Actor> c1 = new List<Actor>();
            c1.Add(a1);
            c1.Add(a2);

            Movie m1 = new Movie
            {
                Id = 1,
                Title = "Sing",
                Description = "Cartoon about animals",
                IsMovie = true,
                ReleaseDate = new DateTime(2021, 01, 01),
                CoverUrl = "www.img.com",
                Ratings = m1Ratings,
                Cast = c1
            };

            Movie m2 = new Movie
            {
                Id = 2,
                Title = "The Bad Boys",
                Description = "About cops in LA",
                IsMovie = true,
                ReleaseDate = new DateTime(2000, 01, 01),
                CoverUrl = "www.img.com",
                Ratings = m2Ratings,
                Cast = c1
            };

            context.Movies.Add(m1);
            context.Movies.Add(m2);
            context.Actors.Add(a1);
            context.Actors.Add(a1);
            context.Ratings.Add(rating1);
            context.Ratings.Add(rating2);
            context.Ratings.Add(rating3);
            context.Ratings.Add(rating4);

            context.SaveChanges();
        }

        //Test searching movie by title
        [Test]
        [TestCase("sing")]
        [TestCase("si")]
        public async Task SearchMovies_InputQueryString_ReturnSingMovie(string query)
        {
            //arrange
            var movieRepository = new MovieRepository(context, _mapper);

            //act
            var movies = await movieRepository.SearchMediaAsync(query);

            //assert
            Assert.That(movies[0].Title.ToLower().Contains("sing"));
        }

        //Testing search by rating - 5 stars
        [Test]
        [TestCase("5 stars")]
        public async Task SearchMovies_InputQueryString5Stars_Return5StarMovies(string query)
        {
            //arrange
            var movieRepository = new MovieRepository(context, _mapper);

            //act
            var movies = await movieRepository.SearchMediaAsync(query);

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(movies[0].Title.ToLower().Contains("sing"));
                Assert.That(movies[0].Ratings.Select(x => x.Value).Average() >= 4.5);
            });
        }

        //Testing search by at least keyword
        //Search should return all >= 4 star movies ordered by rating
        [Test]
        [TestCase("at least 4 stars")]
        public async Task SearchMovies_InputQueryStringatleast4Stars_Return4and5StarMovies(string query)
        {
            //arrange
            var movieRepository = new MovieRepository(context, _mapper);

            //act
            var movies = await movieRepository.SearchMediaAsync(query);

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(movies[0].Title.ToLower().Contains("sing"));
                Assert.That(movies[1].Title.ToLower().Contains("bad boys"));
                Assert.That(movies.Count == 2);
                Assert.That(movies[0].Ratings.Select(x => x.Value).Average() >= 4);
                Assert.That(movies[1].Ratings.Select(x => x.Value).Average() >= 4);
            });
        }

        //Testing search by 4 star keyword
        //All movies that have 4 stars should be returned (but not movies with 5 stars)
        [Test]
        [TestCase("4 stars")]
        public async Task SearchMovies_InputQueryString4Stars_Return4StarMovies(string query)
        {
            //arrange
            var movieRepository = new MovieRepository(context, _mapper);

            //act
            var movies = await movieRepository.SearchMediaAsync(query);

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(movies[0].Title.ToLower().Contains("bad boys"));
                Assert.That(movies.Count == 1);
                Assert.That(movies[0].Ratings.Select(x => x.Value).Average() >= 4);
            });
        }

        //Test search by after keyword
        //Search should return movies release year > query year 
        [Test]
        [TestCase("after 2010")]
        [TestCase("after 2020")]
        public async Task SearchMovies_InputQueryStringAfter2020_ReturnSingMovieReleasedIn2021(string query)
        {
            //arrange
            var movieRepository = new MovieRepository(context, _mapper);

            //act
            var movies = await movieRepository.SearchMediaAsync(query);

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(movies[0].Title.ToLower().Contains("sing"));
                Assert.That((movies[0].ReleaseDate.Year > 2000));
            });
        }

        //Test search by year
        //Should return movies release on query year
        [Test]
        [TestCase("2000")]
        public async Task SearchMovies_InputQueryStringYear2000_ReturnBadBoysMovieReleased2000(string query)
        {
            //arrange
            var movieRepository = new MovieRepository(context, _mapper);

            //act
            var movies = await movieRepository.SearchMediaAsync(query);

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(movies[0].Title.ToLower().Contains("bad boys"));
                Assert.That((movies[0].ReleaseDate.Year == 2000));
            });
        }

        //Test search by description
        [Test]
        [TestCase("cops")]
        [TestCase("in LA")]
        public async Task SearchMovies_InputQueryStringDescription_ReturnMovieAboutCops(string query)
        {
            //arrange
            var movieRepository = new MovieRepository(context, _mapper);

            //act
            var movies = await movieRepository.SearchMediaAsync(query);

            //assert
            Assert.That(movies[0].Title.ToLower().Contains("bad boys"));
        }
    }
}
