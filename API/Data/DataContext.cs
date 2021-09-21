using API.Entity;
using API.Entity.StoredProceduresEntites;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Top10Item>().HasNoKey();
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Screening> Screenings { get; set; }

        //Stored Procedures Keyless Tables
        public DbSet<Top10Item> TopRatedMovies { get; set; }


    }
}