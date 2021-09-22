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
    public class ReportService : IReportService
    {
        public DataContext _context { get; }
        public ReportService(DataContext context)
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

        public async Task<ServiceResponse<List<TopScreened>>> GetTop10ScreenedAsync(DateTime startDate, DateTime endDate)
        {
            var response = new ServiceResponse<List<TopScreened>>();
            try
            {
                response.Data =  await _context.TopScreenedMovies
                .FromSqlRaw("EXEC [dbo].[spGet_Top10_ScreenedMovies] {0}, {1};", startDate, endDate).ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<TopSold>>> GetTopSoldAsync()
        {
            var response = new ServiceResponse<List<TopSold>>();
            try
            {
                response.Data =  await _context.TopSoldMovies
                .FromSqlRaw("EXEC [dbo].[spGet_Top_Sold_Movies];").ToListAsync();
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