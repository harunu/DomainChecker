using DomainChecker.Data;
using DomainChecker.Data.Requests;
using DomainChecker.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DomainChecker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DomainsController : ControllerBase
    {
        private readonly IDomainService _domainService;
        private readonly ILogger<DomainsController> _logger;

        public DomainsController(IDomainService domainService, ILogger<DomainsController> logger)
        {
            _domainService = domainService;
            _logger = logger;
        }

        [HttpGet("check")]
        public async Task<ActionResult<DomainDto>> CheckAvailability([FromQuery] string domainName)
        {
            _logger.LogInformation($"Checking availability for domain: {domainName}");
            try
            {
                var domain = await _domainService.CheckDomainAvailabilityAsync(domainName);
                if (domain == null)
                {
                    _logger.LogWarning($"Domain information for {domainName} could not be retrieved.");
                    return NotFound($"Information for {domainName} could not be retrieved.");
                }
                return Ok(domain);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking availability for domain: {domainName}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("test")]
        public ActionResult Test()
        {
            return Ok("Route test successful.");
        }

        [HttpPost("addToFavorites")]
        public async Task<IActionResult> AddToFavorites([FromBody] AddToFavoritesRequest request)
        {
            if (request == null)
            {
                _logger.LogWarning("Add to favorites request body is null.");
                return BadRequest("Request body is null");
            }

            try
            {
                await _domainService.AddDomainToFavoritesAsync(request.DomainName, request.IsAvailable, request.ExpiryDate);
                return Ok("Added to favorites successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding domain to favorites: {request.DomainName}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("removeFromFavorites/{id}")]
        public async Task<IActionResult> RemoveFromFavorites(int id)
        {
            try
            {
                await _domainService.RemoveDomainFromFavoritesAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing domain from favorites: {id}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("favorites")]
        public async Task<ActionResult<IEnumerable<DomainDto>>> GetFavorites()
        {
            try
            {
                var favorites = await _domainService.GetFavoriteDomainsAsync();
                return Ok(favorites);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving favorite domains.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
