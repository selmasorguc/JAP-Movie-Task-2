namespace APITests
{
    using API.Data;
    using API.DTOs;
    using API.Entity;
    using API.Helpers;
    using API.Services;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    [TestFixture]
    public class ScreeningStartTimeNUnitTests
    {
        private Screening screening1;

        private User user;

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

            screening1 = new Screening
            {
                Id = 1,
                MovieId = 1,
                MaxSeatsNumber = 20
            };

            using var hmac = new HMACSHA512();

            user = new User
            {
                Id = 1,
                Username = "selma",
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("selma1")),
                PasswordSalt = hmac.Key
            };

            var mappingConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new AutoMapperProfiles());
            });
            _mapper = mappingConfig.CreateMapper();
        }

        [Test]
        [TestCase("01/20/2012")]
        [TestCase("01/20/2021")]
        [TestCase("09/09/2021")]

        public async Task ScreeningDate_DateNotInFuture_ThrowException(DateTime date)
        {
            //arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                          .UseInMemoryDatabase(databaseName: "temp-movieapp").Options;
            var context = new DataContext(options);
            context.Database.EnsureDeleted();
            context.Movies.Add(movie1);
            screening1.StartTime = date;
            context.Screenings.Add(screening1);
            context.Users.Add(user);
            context.SaveChanges();
            var ticket1 = new TicketDto
            {
                MovieId = movie1.Id,
                Price = 10.0,
                ScreeningId = screening1.Id
            };

            //act
            var ticketService = new TicketService(context, _mapper);
            ServiceResponse<AddTicketDto> response = await ticketService.BuyTicket(ticket1, user.Username);

            //assert
            Assert.That(response.Success == false);
            Assert.That(response.Message.Equals("Screening is in the past"));
        }
    }
}
