using AutoMapper;
using BookstoreApplication.DTO;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookstoreApplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(RegistrationDto data) 
        {
            var user = _mapper.Map<ApplicationUser>(data);
            var result = await _userManager.CreateAsync(user, data.Password);
            
            if (!result.Succeeded)
            {
                string errorMessage = string.Join("; ", result.Errors.Select(e=>e.Description));
                throw new BadRequestException(errorMessage);
            }

            if (!await _userManager.IsInRoleAsync(user, "Librarian"))
            {
                await _userManager.AddToRoleAsync(user, "Librarian");
            }
            var token = await GenerateJwt(user);
            return token;

        }

        public async Task<string> LoginAsync(LoginDto loginData) 
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

            var token = await GenerateJwt(user);
            return token;
        }

        private async Task<string> GenerateJwt(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim("username", user.UserName),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim("role", role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims:claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       public async Task<ProfileDto> GetProfile(ClaimsPrincipal principalUser) 
        {
            var username = principalUser.FindFirstValue("username");
            if (username == null)
            {
                throw new BadRequestException("Invalid token");
            }
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) 
            {
                throw new NotFoundException("User with provided username does not exist.");
            }
            return _mapper.Map<ProfileDto>(user);
        }

        public async Task<string> LoginWithGoogle(string email, string? name, string? surname) 
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    Name = name,
                    Surname = surname
                };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Librarian");
                }
                else 
                {
                    throw new BadRequestException("Google login failed: user creation error");
                }
            }
            var token = await GenerateJwt(user);
            return token;
        }
    }
}
