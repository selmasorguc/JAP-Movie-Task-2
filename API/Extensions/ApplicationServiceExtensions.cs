namespace API.Extensions
{
    using API.Data;
    using API.Helpers;
    using API.Interfaces;
    using API.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}
