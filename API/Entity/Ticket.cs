namespace API.Entity
{
    public class Ticket
    {
        public int Id { get; set; } 
        public double Price { get; set; }
        public Screening Screening { get; set; }
        public int ScreeningId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
    }
}