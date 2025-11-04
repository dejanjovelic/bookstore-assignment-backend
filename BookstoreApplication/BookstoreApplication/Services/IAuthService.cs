using BookstoreApplication.DTO;
using System.Security.Claims;

namespace BookstoreApplication.Services
{
    public interface IAuthService
    {
        Task<ProfileDto> GetProfile(ClaimsPrincipal principalUser);
        Task<string> LoginAsync(LoginDto loginData);
        Task RegisterAsync(RegistrationDto data);
    }
}
