using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.DTOs.UserDtos;
using API.Entity;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly SymmetricSecurityKey _key;

        public AuthRepository(DataContext dataContext, IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            _context = dataContext;
        }

        public async Task<RegisterDto> Login(LogInDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user != null)
            {
                using var hmac = new HMACSHA512(user.PasswordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.PasswordHash[i]) return null;
                }

                return new RegisterDto
                {
                    Username = loginDto.Username,
                    Token = CreateToken(user)
                };
            }
            return null;
        }

        public async Task<RegisterDto> Register(string username, string password)
        {
            if (await UserExists(username, password)) return null;

            using var hmac = new HMACSHA512();

            var user = new User
            {
                Username = username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new RegisterDto
            {
                Username = username,
                Token = CreateToken(user)
            };
        }

        public async Task<bool> UserExists(string username, string password)
        {
            return await _context.Users.AnyAsync(x => x.Username == username);
        }

        
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Username)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}