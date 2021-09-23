using System;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entity;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class TicketService : ITicketService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public TicketService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        //Buying a ticket for an existing screening
        public async Task<ServiceResponse<AddTicketDto>> BuyTicket(TicketDto ticket, string username)
        {
            var serviceResponse = new ServiceResponse<AddTicketDto>();

            try
            {
                //Check if movie and user exist in the DB
                if (await _context.Movies.FirstOrDefaultAsync(x => x.Id == ticket.MovieId) == null)
                {
                    throw new ArgumentException(
                        "Movie id is not valid. Are you sure this movie exists in the database?");
                }

                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

                //Check if the screening for that movie exist in the DB
                var screening = await _context.Screenings
                .FirstOrDefaultAsync(x => x.Id == ticket.ScreeningId);

                if (screening == null)
                    throw new ArgumentException(
                           "Screening id is not valid. Are you sure this screening exists in the database?");
               
                if (screening.MaxSeatsNumber == 0)
                    throw new ArgumentException(
                           "No available seats. Tickets sold out.");

                if (screening.StartTime < DateTime.Today)
                    throw new ArgumentException(
                           "Screening is in the past");

                //if the screening exists, add the new ticket and update the screening in the DB


                screening.MaxSeatsNumber -= 1;
                _context.Screenings.Update(screening);

                var addTicket = new AddTicketDto
                {
                    Price = ticket.Price,
                    MovieId = ticket.MovieId,
                    UserId = user.Id,
                    ScreeningId = screening.Id
                };

                _context.Tickets.Add(_mapper.Map<Ticket>(addTicket));

                await _context.SaveChangesAsync();
                serviceResponse.Data = addTicket;
            }
            catch (Exception ex)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<AddScreeningDto>> CreateScreening(AddScreeningDto screening)
        {
            var serviceResponse = new ServiceResponse<AddScreeningDto>();
            try
            {
                //Check if movie and user exist in the DB
                if (await _context.Movies.FirstOrDefaultAsync(x => x.Id == screening.MovieId) == null)
                {
                    throw new ArgumentException(
                        "Movie id is not valid. Are you sure this movie exists in the database?");
                }
                _context.Screenings.Add(_mapper.Map<Screening>(screening));

                await _context.SaveChangesAsync();
                serviceResponse.Data = screening;
            }
            catch (Exception ex)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
    }
}