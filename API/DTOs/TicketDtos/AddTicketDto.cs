namespace API.DTOs
{
    public class AddTicketDto
    {
        public double Price { get; set; }

        public int ScreeningId { get; set; }

        public int UserId { get; set; }

        public int MovieId { get; set; }
    }
}
