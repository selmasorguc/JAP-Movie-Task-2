namespace API.Interfaces
{
    using API.DTOs;
    using API.Entity;
    using System.Threading.Tasks;

    public interface ITicketService
    {
        Task<ServiceResponse<AddTicketDto>> BuyTicket(TicketDto ticket, string username);

        Task<ServiceResponse<AddScreeningDto>> CreateScreening(AddScreeningDto screening);
    }
}
