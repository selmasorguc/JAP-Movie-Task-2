using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entity;

namespace API.Interfaces
{
    public interface ITicketService
    {
        Task<ServiceResponse<AddTicketDto>> BuyTicket(TicketDto ticket, string username);
        Task<ServiceResponse<AddScreeningDto>> CreateScreening(AddScreeningDto screening);
    }
}