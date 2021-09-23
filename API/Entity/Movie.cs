namespace API.Entity
{
    using System;
    using System.Collections.Generic;

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

        public IEnumerable<Screening> Screenings { get; set; }
    }
}
