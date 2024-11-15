using Assesment_DiegoFelipeSalamancaRojas.DTOs;
using System.Threading.Tasks;

namespace Assesment_DiegoFelipeSalamancaRojas.Interfaces
{
    public interface IAuthService
{
    Task<AuthResultDto> RegisterDoctorAsync(DoctorRegistrationDto dto);
    Task<AuthResultDto> RegisterPatientAsync(PatientRegistrationDto dto);
    Task<AuthResultDto> LoginDoctorAsync(DoctorLoginDto dto); // Para login de doctor
    Task<AuthResultDto> LoginPatientAsync(PatientLoginDto dto); // Para login de paciente
}

}
