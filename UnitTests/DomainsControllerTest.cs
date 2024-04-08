using DomainChecker.Controllers;
using DomainChecker.Data;
using DomainChecker.Data.Requests;
using DomainChecker.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests
{
    public class DomainsControllerTests
    {
        private readonly DomainsController _controller;
        private readonly Mock<IDomainService> _domainServiceMock = new();
        private readonly Mock<ILogger<DomainsController>> _loggerMock = new();

        public DomainsControllerTests()
        {
            _controller = new DomainsController(_domainServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CheckAvailability_ExistingDomain_ReturnsOk()
        {
            // Arrange
            var domainName = "existingdomain.com";
            var domainDto = new DomainDto { Name = domainName, IsAvailable = false };

            _domainServiceMock.Setup(s => s.CheckDomainAvailabilityAsync(domainName))
                .ReturnsAsync(domainDto);

            // Act
            var result = await _controller.CheckAvailability(domainName);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedDomain = okResult.Value.Should().BeAssignableTo<DomainDto>().Subject;
            returnedDomain.Name.Should().Be(domainName);
        }

        [Fact]
        public async Task AddToFavorites_ValidDomain_AddsSuccessfully()
        {
            // Arrange
            var request = new AddToFavoritesRequest { DomainName = "test.com", IsAvailable = true, ExpiryDate = DateTime.UtcNow.AddDays(365) };
            _domainServiceMock.Setup(s => s.AddDomainToFavoritesAsync(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<DateTime?>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddToFavorites(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task RemoveFromFavorites_ValidId_RemovesSuccessfully()
        {
            // Arrange
            int domainId = 1;
            _domainServiceMock.Setup(s => s.RemoveDomainFromFavoritesAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.RemoveFromFavorites(domainId);

            // Assert
            result.Should().BeOfType<OkResult>();
        }
        [Fact]
        public async Task GetFavorites_ReturnsListOfFavorites()
        {
            // Arrange
            var favorites = new List<DomainDto>
          {
             new() { Name = "favorite1.com", IsFavorite = true },
              new() { Name = "favorite2.com", IsFavorite = true }
          };
            _domainServiceMock.Setup(s => s.GetFavoriteDomainsAsync())
                .ReturnsAsync(favorites);

            // Act
            var result = await _controller.GetFavorites();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedFavorites = okResult.Value.Should().BeAssignableTo<IEnumerable<DomainDto>>().Subject;
            returnedFavorites.Count().Should().Be(2);
        }
    }
}
