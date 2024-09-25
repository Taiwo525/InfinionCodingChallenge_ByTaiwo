using InfinionProduct_Application.DTOs.AuthDTOs;
using InfinionProduct_Core.Entities;


namespace InfinionProduct_Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegistrationRequestDto model);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto model);
        Task<AuthResponseDto> ConfirmEmailAsync(string id, string token);
        Task<string> GenerateEmailConfirmationTokenAsync(User user);
    }
}
