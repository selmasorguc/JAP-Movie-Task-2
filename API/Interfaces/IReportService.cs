namespace API.Interfaces
{
    using API.Entity;
    using API.Entity.StoredProceduresEntites;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReportService
    {
        Task<ServiceResponse<List<Top10Item>>> GetTop10MoviesAsync();

        Task<ServiceResponse<List<TopScreened>>> GetTop10ScreenedAsync(DateTime startDate, DateTime endDate);

        Task<ServiceResponse<List<TopSold>>> GetTopSoldAsync();
    }
}
