using API.Entity;

namespace API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}