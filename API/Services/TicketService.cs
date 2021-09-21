using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ServiceResponse<AddTicketDto>> BuyTicket(TicketDto ticket, string username)
        {
            var serviceResponse = new ServiceResponse<AddTicketDto>();

            try
            {
                //or I could use SingleOrDefaultAsync and just catch general error message 
                if (await _context.Movies.FirstOrDefaultAsync(x => x.Id == ticket.MovieId) == null)
                {
                    throw new ArgumentException(
                        "Movie id is not valid. Are you sure this movie exists in the database?");
                }
                
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
                if (user == null )
                    throw new ArgumentException(
                           "User id is not valid. Are you sure this user exists in the database?");
                //---
                
                var screening = await _context.Screenings
                .FirstOrDefaultAsync(x => x.Id == ticket.ScreeningId);

                if (screening == null)
                    throw new ArgumentException(
                           "Screening id is not valid. Are you sure this screening exists in the database?");

                screening.MaxSeatsNumber -= 1;
                _context.Screenings.Update(screening);

                var addTicket = new AddTicketDto{
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

       
    }
}