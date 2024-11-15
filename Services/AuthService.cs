using Assesment_DiegoFelipeSalamancaRojas.Interfaces;
using Assesment_DiegoFelipeSalamancaRojas.DTOs;
using Assesment_DiegoFelipeSalamancaRojas.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Assesment_DiegoFelipeSalamancaRojas.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<IdentityUser> userManager,
                           SignInManager<IdentityUser> signInManager,
                           RoleManager<IdentityRole> roleManager,
                           IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<AuthResultDto> RegisterDoctorAsync(DoctorRegistrationDto dto)
        {
            var user = new IdentityUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            // Intentar crear el doctor como un usuario de Identity
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return new AuthResultDto
                {
                    Success = false,
                    Message = "Error registering doctor."
                };
            }

            // Asignar el rol de "Doctor"
            if (!await _roleManager.RoleExistsAsync("Doctor"))
            {
                var role = new IdentityRole("Doctor");
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, "Doctor");

            return new AuthResultDto
            {
                Success = true,
                Message = "Doctor registered successfully."
            };
        }

        public async Task<AuthResultDto> RegisterPatientAsync(PatientRegistrationDto dto)
        {
            var user = new IdentityUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            // Intentar crear el paciente como un usuario de Identity
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return new AuthResultDto
                {
                    Success = false,
                    Message = "Error registering patient."
                };
            }

            // Asignar el rol de "Patient"
            if (!await _roleManager.RoleExistsAsync("Patient"))
            {
                var role = new IdentityRole("Patient");
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, "Patient");

            return new AuthResultDto
            {
                Success = true,
                Message = "Patient registered successfully."
            };
        }

        public async Task<AuthResultDto> LoginDoctorAsync(DoctorLoginDto dto)
{
    var user = await _userManager.FindByEmailAsync(dto.Email);

    if (user == null || !await _userManager.IsInRoleAsync(user, "Doctor"))
    {
        return new AuthResultDto
        {
            Success = false,
            Message = "Invalid credentials or user is not a doctor."
        };
    }

    var result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);

    if (!result.Succeeded)
    {
        return new AuthResultDto
        {
            Success = false,
            Message = "Invalid credentials."
        };
    }

    var token = await GenerateJwtTokenAsync(user);

    return new AuthResultDto
    {
        Success = true,
        Message = "Login successful.",
        Token = token
    };
}

public async Task<AuthResultDto> LoginPatientAsync(PatientLoginDto dto)
{
    var user = await _userManager.FindByEmailAsync(dto.Email);

    if (user == null || !await _userManager.IsInRoleAsync(user, "Patient"))
    {
        return new AuthResultDto
        {
            Success = false,
            Message = "Invalid credentials or user is not a patient."
        };
    }

    var result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);

    if (!result.Succeeded)
    {
        return new AuthResultDto
        {
            Success = false,
            Message = "Invalid credentials."
        };
    }

    var token = await GenerateJwtTokenAsync(user);

    return new AuthResultDto
    {
        Success = true,
        Message = "Login successful.",
        Token = token
    };
}


        private async Task<string> GenerateJwtTokenAsync(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("role", string.Join(",", roles)) // Incluir roles en el token
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
