using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entity;
using API.Entity.StoredProceduresEntites;

namespace API.Interfaces
{
    public interface IReportService
    {
        Task<ServiceResponse<List<Top10Item>>> GetTop10MoviesAsync();
        Task<ServiceResponse<List<TopScreened>>> GetTop10ScreenedAsync(DateTime startDate, DateTime endDate);
        Task<ServiceResponse<List<TopSold>>> GetTopSoldAsync();
    }
}