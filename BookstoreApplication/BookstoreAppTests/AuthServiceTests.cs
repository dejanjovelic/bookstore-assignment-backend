using AutoMapper;
using BookstoreApplication.Models;
using BookstoreApplication.Services;
using BookstoreApplication.Services.DTO;
using BookstoreApplication.Services.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreAppTests
{
    public class AuthServiceTests

    {

        [Fact]
        public async Task RegisterAsync_ReturnsToken_WhenNewUserIsRegistrated()
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

            ApplicationUser applicationUser = new ApplicationUser
            {
                Email = registrationDto.Email,
                Name = registrationDto.Name,
                Surname = registrationDto.Surname,
                PasswordHash = registrationDto.Password,
                UserName = registrationDto.Username
            };
            IList<string> roles = new List<string> { "Librarian" };

            var mockMapper = Substitute.For<IMapper>();
            mockMapper.Map<ApplicationUser>(registrationDto).Returns(applicationUser);

            var mockConfiguration = Substitute.For<IConfiguration>();
            mockConfiguration["Jwt:Secret"].Returns("fake-secret");
            mockConfiguration["Jwt:Issuer"].Returns("fake-issuer");
            mockConfiguration["Jwt:Audience"].Returns("fake-audience");
            mockConfiguration["Jwt:Key"].Returns("this_is_a_very_long_fake_secret_key_123456");



            var mockUserManager = Substitute.For<UserManager<ApplicationUser>>(
                Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
                );
            mockUserManager.CreateAsync(applicationUser, registrationDto.Password)
                            .Returns(IdentityResult.Success);
            mockUserManager.IsInRoleAsync(applicationUser, "Librarian")
                .Returns(false);
            mockUserManager.GetRolesAsync(applicationUser).Returns(roles);

            var service = new AuthService(mockUserManager, mockMapper, mockConfiguration);

            //ACT
            var result = await service.RegisterAsync(registrationDto);

            //ASSERT
            Assert.NotNull(result);
            mockMapper.Received().Map<ApplicationUser>(registrationDto);
            await mockUserManager.Received(1).CreateAsync(applicationUser, registrationDto.Password);
            await mockUserManager.Received(1).GetRolesAsync(applicationUser);
        }

        [Fact]
        public async Task RegisterAsync_ThrowsBadRequestException_WhenUserDataIsNotValid()
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

            ApplicationUser applicationUser = new ApplicationUser
            {
                Email = registrationDto.Email,
                Name = registrationDto.Name,
                Surname = registrationDto.Surname,
                PasswordHash = registrationDto.Password,
                UserName = registrationDto.Username
            };


            var mockMapper = Substitute.For<IMapper>();
            mockMapper.Map<ApplicationUser>(registrationDto).Returns(applicationUser);

            var mockConfiguration = Substitute.For<IConfiguration>();

            var mockUserManager = Substitute.For<UserManager<ApplicationUser>>(
                Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
                );
            mockUserManager.CreateAsync(applicationUser, registrationDto.Password)
                            .Returns(IdentityResult.Failed(
                                new IdentityError { Code = "DuplicateUserName", Description = "Username already exists" },
                                new IdentityError { Code = "PasswordToShort", Description = "Password must be at least 10 characters long" }
                                ));

            var service = new AuthService(mockUserManager, mockMapper, mockConfiguration);



            //ACT & ASSERT
            BadRequestException badRequestResult = await Assert.ThrowsAsync<BadRequestException>(
                () => service.RegisterAsync(registrationDto));
            badRequestResult.ShouldNotBeNull();
            badRequestResult.Message.ShouldContain("Password must be at least 10 characters long");
            badRequestResult.Message.ShouldContain("Username already exists");
        }

        [Fact]
        public async Task LoginAsync_ReturnsToken_WhenUserCredentialsIsValid()
        {
            //ARRANGE
            LoginDto loginDto = new LoginDto
            {
                Password = "New@pass1",
                Username = "Name121"
            };

            ApplicationUser user = new ApplicationUser
            {
                Email = "test@gmail.com",
                Name = "Name",
                Surname = "Surname",
                PasswordHash = "New@pass1",
                UserName = "Name121",
            };

            IList<string> roles = new List<string> { "Librarian" };

            var mockMapper = Substitute.For<IMapper>();

            var mockConfiguration = Substitute.For<IConfiguration>();
            mockConfiguration["Jwt:Secret"].Returns("fake-secret");
            mockConfiguration["Jwt:Issuer"].Returns("fake-issuer");
            mockConfiguration["Jwt:Audience"].Returns("fake-audience");
            mockConfiguration["Jwt:Key"].Returns("this_is_a_very_long_fake_secret_key_123456");

            var mockUserManager = Substitute.For<UserManager<ApplicationUser>>(
                Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
                );
            mockUserManager.FindByNameAsync(loginDto.Username).Returns(user);
            mockUserManager.CheckPasswordAsync(user, loginDto.Password).Returns(true);
            mockUserManager.GetRolesAsync(user).Returns(roles);

            var service = new AuthService(mockUserManager, mockMapper, mockConfiguration);

            //ACT
            var result = await service.LoginAsync(loginDto);

            //ASSERT
            result.ShouldNotBeNull();
            result.ShouldContain(".");
            mockUserManager.Received(1).FindByNameAsync(user.UserName);
            mockUserManager.Received(1).CheckPasswordAsync(user, loginDto.Password);
            mockUserManager.Received(1).GetRolesAsync(user);
        }

        [Fact]
        public async Task LoginAsync_ThrowBadRequestException_WhenUserNotExist()
        {
            //ARRANGE
            LoginDto loginDto = new LoginDto
            {
                Password = "New@pass1",
                Username = "Name121"
            };

            ApplicationUser user = null;


            var mockMapper = Substitute.For<IMapper>();

            var mockConfiguration = Substitute.For<IConfiguration>();

            var mockUserManager = Substitute.For<UserManager<ApplicationUser>>(
                Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
                );
            mockUserManager.FindByNameAsync(loginDto.Username).Returns(user);

            var service = new AuthService(mockUserManager, mockMapper, mockConfiguration);

            //ACT & ASSERT
            BadRequestException badRequestResult = await Assert.ThrowsAsync<BadRequestException>(() => service.LoginAsync(loginDto));
            badRequestResult.ShouldNotBeNull();
            badRequestResult.Message.ShouldBe("Invalid credentials");

        }

        [Fact]
        public async Task LoginAsync_ThrowBadRequestException_WhenPasswordMismatch()
        {
            //ARRANGE
            LoginDto loginDto = new LoginDto
            {
                Password = "New@pass1",
                Username = "Name121"
            };

            ApplicationUser user = new ApplicationUser
            {
                Email = "test@gmail.com",
                Name = "Name",
                Surname = "Surname",
                PasswordHash = "Nova@Lozinka12",
                UserName = "Name121",
            };

            var mockMapper = Substitute.For<IMapper>();
            var mockConfiguration = Substitute.For<IConfiguration>();

            var mockUserManager = Substitute.For<UserManager<ApplicationUser>>(
                Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
                );
            mockUserManager.FindByNameAsync(loginDto.Username).Returns(user);
            mockUserManager.CheckPasswordAsync(user, loginDto.Password).Returns(false);

            var service = new AuthService(mockUserManager, mockMapper, mockConfiguration);

            //ACT & ASSERT
            BadRequestException badRequestResult = await Assert.ThrowsAsync<BadRequestException>(
                () => service.LoginAsync(loginDto));
            badRequestResult.ShouldNotBeNull();
            badRequestResult.Message.ShouldBe("Invalid credentials");
        }

        [Fact]
        public async Task GetProfile_ReturnsUserProfile_When_User_exist()
        {
            //Arrange
            ProfileDto profileDto = new ProfileDto
            {
                Email = "test@gmail.com",
                Name = "Name",
                Surname = "Surname",
                Username = "Username12"
            };

            ApplicationUser applicationUser = new ApplicationUser
            {
                Email = "test@gmail.com",
                Name = "Name",
                Surname = "Surname",
                PasswordHash = "Nova@Lozinka12",
                UserName = "Username12",
            };

            var claims = new List<Claim>
            {
                new Claim("username", "Username12")
            };

            var identity = new ClaimsIdentity(claims);
            var user = new ClaimsPrincipal(identity);

            var mockUserManager = Substitute.For<UserManager<ApplicationUser>>(
                Substitute.For<IUserStore<ApplicationUser>>(),
                null, null, null, null, null, null, null, null
                );
            var username = profileDto.Username;
            mockUserManager.FindByNameAsync(username).Returns(applicationUser);

            var mockConfiguration = Substitute.For<IConfiguration>();

            var mockMapper = Substitute.For<IMapper>();
            mockMapper.Map<ProfileDto>(applicationUser).Returns(profileDto);

            var service = new AuthService(mockUserManager, mockMapper, mockConfiguration);

            //Act
            var result = await service.GetProfile(user);

            //Assert
            result.ShouldNotBeNull();
            result.Email.ShouldBe(profileDto.Email);
            result.Name.ShouldBe(profileDto.Name);
            result.Surname.ShouldBe(profileDto.Surname);
            mockMapper.Received(1).Map<ProfileDto>(applicationUser);
            mockUserManager.Received(1).FindByNameAsync(username);

        }

        [Fact]
        public async Task GetProfile_ThrowsBadRequestException_When_ClaimsPrincipal_Not_Have_Username()
        {
            //Arrange
            var claims = new List<Claim>();

            var identity = new ClaimsIdentity(claims);
            var user = new ClaimsPrincipal(identity);

            var mockUserManager = Substitute.For<UserManager<ApplicationUser>>(
                Substitute.For<IUserStore<ApplicationUser>>(),
                null, null, null, null, null, null, null, null
                );

            var mockConfiguration = Substitute.For<IConfiguration>();

            var mockMapper = Substitute.For<IMapper>();

            var service = new AuthService(mockUserManager, mockMapper, mockConfiguration);


            //Act & Assert
            BadRequestException badRequestResult = await Assert.ThrowsAsync<BadRequestException>(() => service.GetProfile(user));
            badRequestResult.ShouldNotBeNull();
            badRequestResult.Message.ShouldBe("Invalid token");
        }

        [Fact]
        public async Task GetProfile_ThrowNotFoundException_When_User_Not_Exist()
        {
            //Arrange
            ProfileDto profileDto = new ProfileDto
            {
                Email = "test@gmail.com",
                Name = "Name",
                Surname = "Surname",
                Username = "Username12"
            };

            var claims = new List<Claim>
            {
                new Claim("username", "Username12")
            };

            var identity = new ClaimsIdentity(claims);
            var user = new ClaimsPrincipal(identity);

            var mockUserManager = Substitute.For<UserManager<ApplicationUser>>(
                Substitute.For<IUserStore<ApplicationUser>>(),
                null, null, null, null, null, null, null, null
                );
            var username = profileDto.Username;
            mockUserManager.FindByNameAsync(username).Returns((ApplicationUser)null);

            var mockConfiguration = Substitute.For<IConfiguration>();

            var mockMapper = Substitute.For<IMapper>();

            var service = new AuthService(mockUserManager, mockMapper, mockConfiguration);


            //Act & Assert
            NotFoundException notFoundtResult = await Assert.ThrowsAsync<NotFoundException>(() => service.GetProfile(user));
            notFoundtResult.ShouldNotBeNull();
            notFoundtResult.Message.ShouldBe("User with provided username does not exist.");
        }
    }
}
