using API.Extensions;
using API.DTOs;
using API.Entity;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Movie, MovieDto>();
            CreateMap<Actor, ActorDto>();
            CreateMap<Rating, RatingDto>();
            CreateMap<Ticket, TicketDto>();
            CreateMap<TicketDto, Ticket>();
        }
    }
}