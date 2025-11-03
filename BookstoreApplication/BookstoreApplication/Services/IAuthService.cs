using BookstoreApplication.DTO;

namespace BookstoreApplication.Services
{
    public interface IAuthService
    {
        Task LoginAsync(LoginDto loginData);
        Task RegisterAsync(RegistrationDto data);
    }
}
