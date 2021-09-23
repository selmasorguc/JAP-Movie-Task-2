using System.Threading.Tasks;
using API.DTOs.UserDtos;
using API.Entity;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<ServiceResponse<RegisterDto>>> Register(string username, string password)
        {
            var response = await _authRepo.Register(username, password);
            if(response.Success == false) return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<RegisterDto>>> Login(LogInDto loginDto)
        {
            var response = await _authRepo.Login(loginDto);
            if(response.Success == false) return BadRequest(response);
            return Ok(response);
        }
    }
}