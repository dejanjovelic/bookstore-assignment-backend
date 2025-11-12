using BookstoreApplication.Services.Exceptions;
using BookstoreApplication.Services.DTO;
using BookstoreApplication.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _authService.RegisterAsync(data));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _authService.LoginAsync(data));
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            return Ok(await _authService.GetProfile(User));
        }

        [HttpGet("google-login")]
        public IActionResult GoogleLogin() 
        {
            var properties = new AuthenticationProperties { RedirectUri = "/api/Auth/google-response" };
            return Challenge(properties, "Google");
        }
        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse() 
        {
            var result = await HttpContext.AuthenticateAsync("Google");
            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
            var name = result.Principal.FindFirstValue(ClaimTypes.GivenName);
            var surname = result.Principal.FindFirstValue(ClaimTypes.Surname);

            if (email == null) 
            {
                return BadRequest("Email not found in Google login response.");
            }

            var token = await _authService.LoginWithGoogle(email, name, surname);

            var frontendUrl = $"http://localhost:5173/google-callback?token={token}";
            return Redirect(frontendUrl);
        }
    }
}
