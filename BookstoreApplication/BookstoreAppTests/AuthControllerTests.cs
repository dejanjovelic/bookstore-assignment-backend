using BookstoreApplication.Controllers;
using BookstoreApplication.Services;
using BookstoreApplication.Services.DTO;
using BookstoreApplication.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreAppTests
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task Register_CreatesNewUser_When_Registration_Data_Is_Valid()
        {
            //ARRANGE
            RegistrationDto registrationDto = new RegistrationDto
            {
                Email = "test@gmail.com",
                Name = "Name",
                Surname = "Surname",
                Password = "New@pass1",
                Username = "Name121",
            };

            string token = Guid.NewGuid().ToString();

            var mockAuthService = Substitute.For<IAuthService>();
            mockAuthService.RegisterAsync(registrationDto).Returns(token);

            var controller = new AuthController(mockAuthService);

            //ACT
            var result = await controller.Register(registrationDto);

            //ASSERT
            Assert.NotNull(result);
            result.ShouldBeOfType<OkObjectResult>();
            await mockAuthService.Received().RegisterAsync(registrationDto);
        }

        [Fact]
        public async Task Register_ThrowsBadRequestObjectResult_When_Registration_Data_Is_Not_Valid_And_ModelState_IsNotValid()
        {
            //ARRANGE
            RegistrationDto registrationDto = new RegistrationDto();

            var mockAuthService = Substitute.For<IAuthService>();
            var controller = new AuthController(mockAuthService);

            controller.ModelState.AddModelError("Email", "Email is required");

            //ACT
            var result = await controller.Register(registrationDto);

            //ASSERT
            result.ShouldBeOfType<BadRequestObjectResult>();
            var badRequest = result as BadRequestObjectResult;
            badRequest.ShouldNotBeNull();
            badRequest.Value.ShouldBeOfType<SerializableError>();

            var errors = badRequest.Value as SerializableError;
            errors.ContainsKey("Email").ShouldBeTrue();
            var errorMessages = errors["Email"] as string[];
            errorMessages.ShouldNotBeNull();
            errorMessages.ShouldContain("Email is required");
        }

        [Fact]
        public async Task Login_ReturnsJwtToken_When_Login_Data_Is_Valid()
        {
            //ARRANGE
            LoginDto loginDto = new LoginDto
            {
                Password = "New@pass1",
                Username = "Name121"
            };

            string token = Guid.NewGuid().ToString();

            var mockAuthService = Substitute.For<IAuthService>();
            mockAuthService.LoginAsync(loginDto).Returns(token);

            var controller = new AuthController(mockAuthService);

            //ACT
            var result = await controller.LoginAsync(loginDto);

            //ASSERT
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OkObjectResult>();
            await mockAuthService.Received(1).LoginAsync(loginDto);
            OkObjectResult okObject = result as OkObjectResult;
            okObject.ShouldNotBeNull();
            okObject.Value.ShouldBe(token);
        }

        [Fact]
        public async Task Login_ThrowsBadRequestObjectResult_When_Login_Data_Is_Not_Valid_And_ModelState_IsNotValid()
        {
            //ARRANGE
            LoginDto loginDto = new LoginDto();
            var mockAuthService = Substitute.For<IAuthService>();
            var controller = new AuthController(mockAuthService);

            controller.ModelState.AddModelError("Username", "Username is required");

            //ACT
            var result = await controller.LoginAsync(loginDto);

            //ASSERT
            result.ShouldBeOfType<BadRequestObjectResult>();
            var badRequest = result as BadRequestObjectResult;
            badRequest.ShouldNotBeNull();
            badRequest.Value.ShouldBeOfType<SerializableError>();

            var error = badRequest.Value as SerializableError;
            error.ShouldNotBeNull();
            error.ContainsKey("Username");

            var errorMessages = error["Username"] as string[];
            errorMessages.ShouldNotBeNull();
            errorMessages.ShouldContain("Username is required");
        }

        [Fact]
        public async Task GetProfile_ReturnsProfileDto()
        {
            ProfileDto profileDto = new ProfileDto
            {
                Email = "test@gmail.com",
                Name = "Name",
                Surname = "Surname",
                Username = "Username12"
            };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "123"),
                new Claim(ClaimTypes.Name, "Name"),
                new Claim(ClaimTypes.Surname, "Surname"),
                new Claim(ClaimTypes.Email, "test@gmail.com")
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            var mockAuthService = Substitute.For<IAuthService>();
            mockAuthService.GetProfile(user).Returns(profileDto);

            var controller = new AuthController(mockAuthService)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext { User = user }
                }
            };

            var result = await controller.GetProfile();

            result.ShouldNotBeNull();
            result.ShouldBeOfType<OkObjectResult>();
            var profileData = result as OkObjectResult;
            profileData.Value.ShouldBeEquivalentTo(profileDto);
            await mockAuthService.Received(1).GetProfile(user);

        }

        [Fact]
        public void GoogleLogin_ReturnsChallengeResult_WithGoogleScheme()
        {
            // ARRANGE
            var mockAuthService = Substitute.For<IAuthService>();
            var controller = new AuthController(mockAuthService);

            // ACT
            var result = controller.GoogleLogin();

            // ASSERT
            result.ShouldBeOfType<ChallengeResult>();
            var challengeResult = result as ChallengeResult;
            challengeResult.ShouldNotBeNull();

            // Proverava da li je autentifikaciona sema Google
            challengeResult.AuthenticationSchemes.ShouldContain("Google");

            // Proverava da li je putanja odgovora postavljena i da li je: "/api/Auth/google-response"
            challengeResult.Properties.ShouldNotBeNull();
            challengeResult.Properties.RedirectUri.ShouldBe("/api/Auth/google-response");
        }

        [Fact]
        public async Task GoogleResponse_UserLogin_When_Google_Return_Valid_Token()
        {
            //ARRANGE

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, "test@gmail.com"),
                new Claim(ClaimTypes.GivenName, "Name"),
                new Claim(ClaimTypes.Surname, "Surname")
            };

            var identity = new ClaimsIdentity(claims);
            var user = new ClaimsPrincipal(identity);

            var authResult = AuthenticateResult.Success(
                new AuthenticationTicket(user, "Google")
                );

            var token = Guid.NewGuid().ToString();
            var mockAuthService = Substitute.For<IAuthService>();
            mockAuthService.LoginWithGoogle("test@gmail.com", "Name", "Surname")
                           .Returns($"{token}");
            var controller = new AuthController(mockAuthService);
            var httpContext = new DefaultHttpContext();

            //Mockovanje IAuthenticationService ugradjenog interfejsa
            var mockAuthenticationService = Substitute.For<IAuthenticationService>();
            mockAuthenticationService.AuthenticateAsync(httpContext, "Google")
                                     .Returns(authResult);
            //Ubacivanje mokovanog IAuthenticationSevice
            httpContext.RequestServices = new ServiceCollection()
                .AddSingleton(mockAuthenticationService)
                .BuildServiceProvider();

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            //ACT

            var result = await controller.GoogleResponse();

            //ASSERT
            Assert.NotNull(result);
            result.ShouldBeOfType<RedirectResult>();
            var redirect = result as RedirectResult;
            redirect.Url.ShouldBe($"http://localhost:5173/google-callback?token={token}");

        }

        [Fact]
        public async Task GoogleResponse_ReturnUnauthorized_When_Google_Login_Fails()
        {
            //ARRANGE

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, "test@gmail.com"),
                new Claim(ClaimTypes.GivenName, "Name"),
                new Claim(ClaimTypes.Surname, "Surname")
            };

            var identity = new ClaimsIdentity(claims);
            var user = new ClaimsPrincipal(identity);

            var authResult = AuthenticateResult.Fail("Google authentication failed");

            var mockAuthService = Substitute.For<IAuthService>();

            var controller = new AuthController(mockAuthService);
            var httpContext = new DefaultHttpContext();

            //Mockovanje IAuthenticationService ugradjenog interfejsa
            var mockAuthenticationService = Substitute.For<IAuthenticationService>();
            mockAuthenticationService.AuthenticateAsync(httpContext, "Google")
                                     .Returns(AuthenticateResult.Fail("Invalid credentials"));
            //Ubacivanje mokovanog IAuthenticationSevice
            httpContext.RequestServices = new ServiceCollection()
                .AddSingleton(mockAuthenticationService)
                .BuildServiceProvider();

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            //ACT
            var result = await controller.GoogleResponse();

            //ASSERT
            result.ShouldBeOfType<UnauthorizedResult>()
                  .StatusCode.ShouldBe(401);
        }


        [Fact]
        public async Task GoogleResponse_ReturnBadRequest_When_Email_Is_Not_Found_In_Google_Response()
        {
            //ARRANGE

            var claims = new List<Claim>();

            var identity = new ClaimsIdentity(claims);
            var user = new ClaimsPrincipal(identity);

            var authResult = AuthenticateResult.Success(
                new AuthenticationTicket(user, "Google")
                );

            var mockAuthService = Substitute.For<IAuthService>();

            var controller = new AuthController(mockAuthService);
            var httpContext = new DefaultHttpContext();

            //Mockovanje IAuthenticationService ugradjenog interfejsa
            var mockAuthenticationService = Substitute.For<IAuthenticationService>();
            mockAuthenticationService.AuthenticateAsync(httpContext, "Google")
                                     .Returns(authResult);
            //Ubacivanje mokovanog IAuthenticationSevice
            httpContext.RequestServices = new ServiceCollection()
                .AddSingleton(mockAuthenticationService)
                .BuildServiceProvider();

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            //ACT
            var result = await controller.GoogleResponse();

            //ASSERT
            var badRequest = result.ShouldBeOfType<BadRequestObjectResult>();
            badRequest.Value.ShouldBe("Email not found in Google login response.");
            badRequest.StatusCode.ShouldBe(400);

        }
    }
}
