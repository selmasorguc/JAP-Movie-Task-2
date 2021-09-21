using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class TopRatedProcedureEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"CREATE PROCEDURE spGet_Top10_RatedMovies
                                as
                                Begin
                                    SELECT TOP(10) Ratings.MovieId AS MovieId, Movies.Title AS MovieTitle, AVG(Ratings.Value) AS AverageRating, COUNT(Ratings.Value) AS TotalRatings
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
