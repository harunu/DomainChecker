using DomainChecker.Interfaces;
using DomainChecker.Models.Rdap;
using DomainChecker.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Net.Http.Json;

namespace UnitTests
{
    public class DomainServiceTests
    {
        private readonly DomainService _domainService;
        private readonly Mock<IDomainRepository> _repositoryMock = new();
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock = new();
        private readonly Mock<ILogger<DomainService>> _loggerMock = new();

        public DomainServiceTests()
        {
            var httpClient = new HttpClient(new FakeHttpMessageHandler()) { BaseAddress = new System.Uri("https://example.com/") };
            _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _domainService = new DomainService(_repositoryMock.Object, _httpClientFactoryMock.Object, _loggerMock.Object);
        }
        [Fact]
        public async Task CheckDomainAvailabilityAsync_DomainNotFound_ReturnsAvailableDomain()
        {
            // Arrange
            var domainName = "availabledomain.com"; 

            // Act
            var result = await _domainService.CheckDomainAvailabilityAsync(domainName);

            // Assert
            result.Should().NotBeNull();
            result.IsAvailable.Should().BeTrue(); 
        }

        [Fact]
        public async Task CheckDomainAvailabilityAsync_DomainFound_ReturnsTakenDomainWithExpiry()
        {
            // Arrange
            var domainName = "taken.com"; 

            // Act
            var result = await _domainService.CheckDomainAvailabilityAsync(domainName);

            // Assert
            result.Should().NotBeNull();
            result.IsAvailable.Should().BeFalse(); 
            result.ExpiryDate.Should().BeAfter(DateTime.UtcNow); 
        }
        private class FakeHttpMessageHandler : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var uri = request.RequestUri.AbsoluteUri;
                HttpResponseMessage responseMessage;

                if (uri.Contains("availabledomain.com"))
                {
                    // Simulate an available domain with a 404 response
                    responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                else
                {
                    // Simulate a taken domain with RDAP response
                    var rdapResponse = new RdapResponse
                    {
                        LdhName = "taken.com",
                        Events = new List<Event>
                {
                    new Event { EventAction = "expiration", EventDate = DateTime.UtcNow.AddYears(1) }
                }
                    };

                    responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = JsonContent.Create(rdapResponse)
                    };
                }

                return Task.FromResult(responseMessage);
            }
        }
    }
}