using AutoMapper;
using BookstoreApplication.Infrastructure.Repositories;
using BookstoreApplication.Models;
using BookstoreApplication.Models.IRepositoies;
using BookstoreApplication.Services;
using BookstoreApplication.Services.DTO;
using BookstoreApplication.Services.Exceptions;
using BookstoreApplication.Services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookstoreAppTests
{
    public class IssueServiceTests
    {
        [Fact]
        public async Task GetIssuesFromVolumeAsync_Returns_Issues_WhenApiReturnsValidJson()
        {
            // Arrange
            var fakeJson = @"[
                { ""Id"": 1, ""Name"": ""Issue One"" },
                { ""Id"": 2, ""Name"": ""Issue Two"" }
                ]";

            var mockConnection = Substitute.For<IComicVineConnection>();
            mockConnection.Get(Arg.Any<string>()).Returns(fakeJson);

            var settings = new Dictionary<string, string>
            {
                { "ComicVine:BaseUrl", "https://fake.api" },
                { "ComicVine:APIKey", "12345" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            var service = new IssueService(configuration, mockConnection,
                Substitute.For<ILogger<IssueService>>(),
                Substitute.For<IIssuesRepository>(),
                Substitute.For<IMapper>());

            // Act
            var result = await service.GetIssuesFromVolumeAsync(42);

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
            result[0].Id.ShouldBe(1);
            result[0].Name.ShouldBe("Issue One");

        }

        [Fact]
        public async Task CreateAsync_CreateIssueInDatabase_IfIssueDataDtoIsValid()
        {
            // Arrange
            CreateIssueDataDto issueDataDto = new CreateIssueDataDto
            {
                Name = "The Crazy Crime Clown! / The Movie That Killed Batman / The Water Crim…",
                CoverDate = new DateTime(1952, 12, 30, 0, 0, 0, DateTimeKind.Utc),
                IssueNumber = "74",
                ImageUrl = "https://comicvine.gamespot.com/a/uploads/scale_small/0/4/173-796-183-1…",
                Description = "Cover by J.Winslow Mortimer.\"The Crazy Crime Clown!\" written by Alvin…",
                NumberOfPages = 200,
                Price = 20,
                AvailableCopies = 5,
                CreatedDate = DateTime.UtcNow,
                ExternalId = 185
            };

            Issue issue = new Issue
            {
                Id = "69166b995059429102c4f142",
                Name = "The Crazy Crime Clown! / The Movie That Killed Batman / The Water Crim…",
                CoverDate = new DateTime(1952, 12, 30, 0, 0, 0, DateTimeKind.Utc),
                IssueNumber = "74",
                ImageUrl = "https://comicvine.gamespot.com/a/uploads/scale_small/0/4/173-796-183-1…",
                Description = "Cover by J.Winslow Mortimer.\"The Crazy Crime Clown!\" written by Alvin…",
                ExternalId = 185,
                NumberOfPages = 200,
                Price = 20,
                AvailableCopies = 5,
                CreatedDate = DateTime.UtcNow
            };

            var mockMapper = Substitute.For<IMapper>();
            mockMapper.Map<Issue>(issueDataDto).Returns(issue);

            var mockIssuesRepository = Substitute.For<IIssuesRepository>();
            mockIssuesRepository.GetByExternalId(185).Returns((Issue)null);
            mockIssuesRepository.CreateAsync(issue).Returns(issue);

            var service = new IssueService
                (
                Substitute.For<IConfiguration>(),
                Substitute.For<IComicVineConnection>(),
                Substitute.For<ILogger<IssueService>>(),
                mockIssuesRepository,
                mockMapper
                );

            // Act
            var result = await service.CreateAsync(issueDataDto);

            // Assert
            result.ShouldNotBeNull();
            result.IssueNumber.ShouldBe("74");
            result.NumberOfPages.ShouldBe(200);
            result.AvailableCopies.ShouldBe(5);
            mockMapper.Received(1).Map<Issue>(issueDataDto);
            await mockIssuesRepository.Received(1).GetByExternalId(185);    
        }

        [Fact]
        public async Task CreateAsync_ThrowBadRequestException_When_CreateIssueDataDto_IsNull()
        {
            // Arrange
            CreateIssueDataDto issueDataDto = null;

            var service = new IssueService
                (
                Substitute.For<IConfiguration>(),
                Substitute.For<IComicVineConnection>(),
                Substitute.For<ILogger<IssueService>>(),
                Substitute.For<IIssuesRepository>(),
                Substitute.For<IMapper>()
                );

            // Act & Assert

            BadRequestException resultException = await Assert.ThrowsAsync<BadRequestException>(()=>service.CreateAsync(issueDataDto));
            resultException.Message.ShouldBe("Invalid data.");
        }

        [Fact]
        public async Task CreateAsync_ThrowsForbiddenException_IfIssueExistsInDatabase()
        {
            // Arrange
            CreateIssueDataDto issueDataDto = new CreateIssueDataDto
            {
                Name = "The Crazy Crime Clown! / The Movie That Killed Batman / The Water Crim…",
                CoverDate = new DateTime(1952, 12, 30, 0, 0, 0, DateTimeKind.Utc),
                IssueNumber = "74",
                ImageUrl = "https://comicvine.gamespot.com/a/uploads/scale_small/0/4/173-796-183-1…",
                Description = "Cover by J.Winslow Mortimer.\"The Crazy Crime Clown!\" written by Alvin…",
                NumberOfPages = 200,
                Price = 20,
                AvailableCopies = 5,
                CreatedDate = DateTime.UtcNow,
                ExternalId = 185
            };

            Issue issue = new Issue
            {
                Id = "69166b995059429102c4f142",
                Name = "The Crazy Crime Clown! / The Movie That Killed Batman / The Water Crim…",
                CoverDate = new DateTime(1952, 12, 30, 0, 0, 0, DateTimeKind.Utc),
                IssueNumber = "74",
                ImageUrl = "https://comicvine.gamespot.com/a/uploads/scale_small/0/4/173-796-183-1…",
                Description = "Cover by J.Winslow Mortimer.\"The Crazy Crime Clown!\" written by Alvin…",
                ExternalId = 185,
                NumberOfPages = 200,
                Price = 20,
                AvailableCopies = 5,
                CreatedDate = DateTime.UtcNow
            };

            var mockMapper = Substitute.For<IMapper>();

            var mockIssuesRepository = Substitute.For<IIssuesRepository>();
            mockIssuesRepository.GetByExternalId(185).Returns(issue);
            mockIssuesRepository.CreateAsync(issue).Returns(issue);

            var service = new IssueService
                (
                Substitute.For<IConfiguration>(),
                Substitute.For<IComicVineConnection>(),
                Substitute.For<ILogger<IssueService>>(),
                mockIssuesRepository,
                mockMapper
                );

            // Act & Assert

            ForbiddenException resultException = await Assert.ThrowsAsync<ForbiddenException>(() => service.CreateAsync(issueDataDto));
            resultException.Message.ShouldBe("Issue allready exist.");
            await mockIssuesRepository.Received(1).GetByExternalId(185);
            await mockIssuesRepository.DidNotReceive().CreateAsync(issue);
        }
    }
}
