using AutoMapper;
using BookstoreApplication.DTO;
using BookstoreApplication.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task RegisterAsync(RegistrationDto data) 
        {
            var user = _mapper.Map<ApplicationUser>(data);
            var result = await _userManager.CreateAsync(user, data.Password);
            if (!result.Succeeded)
            {
                string errorMessage = string.Join("; ", result.Errors.Select(e=>e.Description));
                throw new BadRequestException(errorMessage);
            }  
        }

        public async Task LoginAsync(LoginDto loginData) 
        {
            var user = await _userManager.FindByNameAsync(loginData.Username);
            if (user == null)
            {
                throw new BadRequestException("Invalid credentials");
            }
            var passwordMatch = await _userManager.CheckPasswordAsync(user, loginData.Password);
            if (!passwordMatch)
            {
                throw new BadRequestException("Invalid credentials");
            }
        }
    }
}
