using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entity;
using API.Entity.StoredProceduresEntites;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class MoviesSPService : IMoviesSPService
    {
        public DataContext _context { get; }
        public MoviesSPService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Top10Item>>> GetTop10MoviesAsync()
        {
            var response = new ServiceResponse<List<Top10Item>>();
            try
            {
                response.Data =  await _context.TopRatedMovies
                .FromSqlRaw("EXEC [dbo].[spGet_Top10_RatedMovies];").ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}