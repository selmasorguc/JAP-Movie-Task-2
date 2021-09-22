using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class ProceduresAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopSoldMovies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    MovieTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketsSold = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            var procedureTopRated = @"CREATE PROCEDURE spGet_Top10_RatedMovies
                                        AS
                                        BEGIN
                                            SELECT TOP(10) Movies.Id as MovieId, Movies.Title as MovieTitle, Count(Ratings.Value) as TotalRatings, AVG(Ratings.Value) as AverageRating
                                            FROM Movies
                                            INNER JOIN dbo.Ratings
                                            ON Movies.Id = dbo.Ratings.MovieId
                                            GROUP BY Movies.Id, Title
                                            HAVING Count(Ratings.Value) >= (SELECT COUNT(Ratings.Value)
                                                        FROM dbo.Ratings
                                                        INNER JOIN dbo.Movies
                                                        ON Movies.Id = dbo.Ratings.MovieId
                                                        GROUP BY Movies.Id
                                                        ORDER BY COUNT(Ratings.Value) DESC
                                                        OFFSET 10 ROWS
                                                        FETCH NEXT 1 ROWS ONLY)
                                            ORDER BY AVG(Ratings.Value) DESC
                                        END";
            migrationBuilder.Sql(procedureTopRated);

            var procedureTopScreened = @"CREATE PROCEDURE spGet_Top10_ScreenedMovies
                                                           @StartDate date,
                                                           @EndDate date
                                        AS
                                        BEGIN
                                                SELECT TOP(10) Movies.Id as MovieId, Movies.Title as MovieTitle, COUNT(Screenings.Id) AS TotalScreenings
	                                            FROM Movies, Screenings
	                                            WHERE Movies.Id = Screenings.MovieId AND Movies.IsMovie = 1
	                                            AND @StartDate <=  Screenings.StartTime
	                                            AND Screenings.StartTime  <= @EndDate
	                                            GROUP BY Movies.Id ,Movies.Title
	                                            ORDER BY COUNT(Screenings.Id) DESC
                                        END";
            migrationBuilder.Sql(procedureTopScreened);

            var procedureTopSold = @"CREATE PROCEDURE spGet_Top_Sold_Movies
                                    AS
                                    BEGIN
                                            SELECT Movies.Id as MovieId, Movies.Title as MovieTitle, Tickets.ScreeningId AS ScreeningId, COUNT(Tickets.Id) as TicketsSold
                                            FROM Movies, Tickets
                                            GROUP BY Movies.Id, Tickets.MovieId, Movies.Title, Tickets.ScreeningId
                                            HAVING (SELECT COUNT(*) FROM Ratings r WHERE r.MovieId = Movies.Id) = 0 AND Movies.Id = Tickets.MovieId
                                            ORDER BY COUNT(Tickets.Id) DESC
                                    END";
            migrationBuilder.Sql(procedureTopSold);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopSoldMovies");
        }
    }
}
