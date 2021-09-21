using Microsoft.EntityFrameworkCore;

namespace API.Entity.StoredProceduresEntites
{
 
    public class Top10Item
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public double AverageRating { get; set; }
        public int TotalRatings { get; set; }
    }
}