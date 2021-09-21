using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class TopRatedMoviesProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"CREATE PROCEDURE spGet_Top10_RatedMovies
                                as
                                Begin
                                    SELECT TOP(10) Ratings.MovieId, Movies.Title, AVG(Ratings.Value) AS average_rating, COUNT(Ratings.Value) AS total_ratings
                                    FROM Ratings
                                    INNER JOIN Movies ON Movies.Id = Ratings.MovieId
                                    GROUP BY Ratings.MovieId, Movies.Title
                                    ORDER BY  AVG(Ratings.Value) DESC;
                                End";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"DROP PROCEDURE spGet_Top10_RatedMovies";
            migrationBuilder.Sql(procedure);
        }
    }
}
