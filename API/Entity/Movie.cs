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

        public List<Rating> Ratings { get; set; }

        public bool IsMovie { get; set; }

        public List<Actor> Cast { get; set; }

        public List<Screening> Screenings { get; set; }
    }
}
