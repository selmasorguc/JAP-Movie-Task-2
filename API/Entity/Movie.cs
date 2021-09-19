using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace API.Entity
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string CoverUrl { get; set; }
        public IEnumerable<Rating> Ratings { get; set; }
        public bool IsMovie { get; set; }
        public IEnumerable<Actor> Cast { get; set; }
        public double AverageRating { get; set; }
        public double GetAverageRating()
        {
            var ratingAvg = this.Ratings.Average(x => x.Value);
            return Math.Round(ratingAvg, 1);
        }

    }
}