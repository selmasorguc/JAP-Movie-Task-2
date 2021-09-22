using API.Entity;

namespace API.DTOs
{
    public class TicketDto
    {
        public double Price { get; set; }
        public int ScreeningId { get; set; }
        public int MovieId { get; set; }
    }
}