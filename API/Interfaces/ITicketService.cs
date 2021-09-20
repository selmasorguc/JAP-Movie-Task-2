using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entity;

namespace API.Interfaces
{
    public interface ITicketService
    {
        Task<ServiceResponse<TicketDto>> BuyTicket(TicketDto ticket);
    }
}