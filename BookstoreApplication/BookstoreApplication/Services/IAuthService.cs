using BookstoreApplication.DTO;
using System.Security.Claims;

namespace BookstoreApplication.Services
{
    public interface IAuthService
    {
        Task<ProfileDto> GetProfile(ClaimsPrincipal principalUser);
        Task<string> LoginAsync(LoginDto loginData);
        Task<string> LoginWithGoogle(string email, string? name, string? surname);
        Task<string> RegisterAsync(RegistrationDto data);
    }
}
