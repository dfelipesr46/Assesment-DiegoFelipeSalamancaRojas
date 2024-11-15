using Assesment_DiegoFelipeSalamancaRojas.DTOs;

namespace Assesment_DiegoFelipeSalamancaRojas.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResultDto> RegisterUserAsync(UserRegistrationDto dto);
        Task<AuthResultDto> LoginUserAsync(UserLoginDto dto);
    }
}
