using System.Collections.Generic;

namespace API.Entity
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Movie> Movies { get; set; }

    }
}