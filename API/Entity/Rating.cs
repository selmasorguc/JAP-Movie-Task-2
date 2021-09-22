namespace API.Entity
{
    public class Rating
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}