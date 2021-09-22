using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entity;
using API.Entity.StoredProceduresEntites;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("report")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _moviesSPService;
        public ReportController(IReportService moviesSPService)
        {
            _moviesSPService = moviesSPService;
        }

        //10 movies with the most ratings, ordered by rating descending 
        [HttpGet("rated")]
        public async Task<ServiceResponse<List<Top10Item>>> GetTopRated()
        {
            var response = new ServiceResponse<List<Top10Item>>();
            response = await _moviesSPService.GetTop10MoviesAsync();
            return response;
        }

        //10 movies with the most screenings ordered by descending
        [HttpGet("screened")]
        public async Task<ServiceResponse<List<TopScreened>>> GetTopScreened(
                                                              DateTime startDate, DateTime endDate)
        {
            var response = new ServiceResponse<List<TopScreened>>();
            response = await _moviesSPService.GetTop10ScreenedAsync(startDate, endDate);
            return response;
        }

        //movies with the most sold tickets and no ratings
        [HttpGet("sold")]
        public async Task<ServiceResponse<List<TopSold>>> GetTopSold()
        {
            var response = new ServiceResponse<List<TopSold>>();
            response = await _moviesSPService.GetTopSoldAsync();
            return response;
        }
    }
}