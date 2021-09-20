using System.Threading.Tasks;
using API.DTOs.UserDtos;

namespace API.Interfaces
{
    public interface IAuthRepository
    {
        Task<RegisterDto> Login(LogInDto loginDto);
        Task<RegisterDto> Register(string username, string password);
        Task<bool> UserExists(string username, string password);
    }
}