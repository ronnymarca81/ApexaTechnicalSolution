using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ApexaTechnicalApi.Data;
using ApexaTechnicalApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace ApexaTechnicalApi.Services
{
    public interface IAuthService
    {
        Task<string> RegisterUserAsync(string email, string password);
        Task<string?> LoginUserAsync(string email, string password);
        IEnumerable<User> GetAllUsers();

    }
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext? _context;
        private readonly IConfiguration? _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> RegisterUserAsync(string email, string password)
        {
            if (_context!.Users.Any(u => u.Email == email))
            {
                throw new Exception("Email already exists.");
            }

            var hashedPassword = HashPassword(password);

            var user = new User
            {
                Email = email,
                Password = hashedPassword
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return GenerateJwtToken(email);
        }

        public async Task<string?> LoginUserAsync(string email, string password)
        {
            var user = _context!.Users.SingleOrDefault(u => u.Email == email);
            if (user == null || !VerifyPassword(password, user.Password!))
            {
                return null;
            }

            return GenerateJwtToken(email);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context!.Users.ToList();
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private string GenerateJwtToken(string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration!["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            var enteredHashedPassword = HashPassword(enteredPassword);
            return enteredHashedPassword == storedHashedPassword;
        }

    }
}