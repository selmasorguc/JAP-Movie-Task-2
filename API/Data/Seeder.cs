using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seeder
    {
        public static async Task SeedData(DataContext context)
        {
            if (await context.Movies.AnyAsync()) return;

            var moviesData = await System.IO.File.ReadAllTextAsync("Data/MovieSeedData.json");
            var movies = JsonSerializer.Deserialize<List<Movie>>(moviesData);
            
            foreach (var movie in movies)
            {
                context.Add(movie);
            }

            await context.SaveChangesAsync();
        }
    }
}