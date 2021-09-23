namespace API.Interfaces
{
    using API.DTOs.UserDtos;
    using API.Entity;
    using System.Threading.Tasks;

    public interface IAuthRepository
    {
        Task<ServiceResponse<RegisterDto>> Login(LogInDto loginDto);

        Task<ServiceResponse<RegisterDto>> Register(string username, string password);

        Task<bool> UserExists(string username, string password);
    }
}
