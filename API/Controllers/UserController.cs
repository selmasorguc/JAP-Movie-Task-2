using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.DTOs.UserDtos;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {

        private readonly IAuthRepository _authRepo;

        public UserController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterDto>> Register(string username, string password)
        {
            var user = await _authRepo.Register(username, password);
            if(user == null) return BadRequest("User already registered");
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<RegisterDto>> Login(LogInDto loginDto)
        {
            var user = await _authRepo.Login(loginDto);
            if (user == null) return BadRequest("Invalid username or password");

            return Ok(user);
        }
    }
}