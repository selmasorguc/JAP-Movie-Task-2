namespace API.DTOs
{
    using System;
    using System.Collections.Generic;

    public class MovieDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string CoverUrl { get; set; }

        public bool IsMovie { get; set; }

        public List<ActorDto> Cast { get; set; }

        public List<RatingDto> Ratings { get; set; }

        public List<ScreeningDto> Screenings { get; set; }
    }
}
