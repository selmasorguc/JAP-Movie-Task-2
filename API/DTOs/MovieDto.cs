using System;
using System.Collections.Generic;
using System.Linq;
using API.Entity;

namespace API.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string CoverUrl { get; set; }
        public bool IsMovie { get; set; }
        public IEnumerable<ActorDto> Cast { get; set; }
        public IEnumerable<Rating> Ratings { get; set; }
        public IEnumerable<Screening> Screenings { get; set; }

    }
}