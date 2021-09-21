using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entity;
using API.Entity.StoredProceduresEntites;

namespace API.Interfaces
{
    public interface IMoviesSPService
    {
        Task<ServiceResponse<List<Top10Item>>> GetTop10MoviesAsync();
    }
}