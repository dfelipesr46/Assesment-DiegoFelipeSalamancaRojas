using Assesment_DiegoFelipeSalamancaRojas.Interfaces;
using Assesment_DiegoFelipeSalamancaRojas.DTOs;
using Assesment_DiegoFelipeSalamancaRojas.Models;
using Assesment_DiegoFelipeSalamancaRojas.Data;
using Microsoft.EntityFrameworkCore;

namespace Assesment_DiegoFelipeSalamancaRojas.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuthResultDto> RegisterUserAsync(UserRegistrationDto dto)
        {
            if (_dbContext.Users.Any(u => u.Email == dto.Email))
            {
                return new AuthResultDto
                {
                    Success = false,
                    Message = "Email is already registered."
                };
            }

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password, // TODO: Hash this password
                Role = dto.Role
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return new AuthResultDto
            {
                Success = true,
                Message = "User registered successfully."
            };
        }

        public async Task<AuthResultDto> LoginUserAsync(UserLoginDto dto)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password); // TODO: Hash match

            if (user == null)
            {
                return new AuthResultDto
                {
                    Success = false,
                    Message = "Invalid credentials."
                };
            }

            return new AuthResultDto
            {
                Success = true,
                Message = "Login successful.",
                Token = "JWT_TOKEN_PLACEHOLDER" // TODO: Generate JWT
            };
        }
    }
}
