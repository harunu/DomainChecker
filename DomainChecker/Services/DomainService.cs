
using DomainChecker.Data;
using DomainChecker.Interfaces;
using DomainChecker.Models.Rdap;
using Newtonsoft.Json;

namespace DomainChecker.Services
{
    public class DomainService : IDomainService
    {
        private readonly IDomainRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<DomainService> _logger;

        public DomainService(IDomainRepository repository, IHttpClientFactory httpClientFactory, ILogger<DomainService> logger)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<DomainDto?> CheckDomainAvailabilityAsync(string domainName)
        {
            _logger.LogInformation($"Checking domain availability for {domainName}");
            var httpClient = _httpClientFactory.CreateClient();
            string rdapUrl = $"https://rdap.nicproxy.com/domain/{domainName}/";
            try
            {
                var response = await httpClient.GetAsync(rdapUrl);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new DomainDto
                    {
                        Name = domainName.Trim(),
                        IsAvailable = true,
                        LastChecked = DateTime.UtcNow,
                        IsFavorite = false
                    };
                }
                else if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var rdapResponse = JsonConvert.DeserializeObject<RdapResponse>(jsonResponse);

                    DateTime? expirationDate = rdapResponse?.Events?.FirstOrDefault(e => e.EventAction == "expiration")?.EventDate;

                    return new DomainDto
                    {
                        Name = rdapResponse?.LdhName,
                        IsAvailable = false,
                        LastChecked = DateTime.UtcNow,
                        ExpiryDate = expirationDate,
                        IsFavorite = false
                    };
                }

                _logger.LogWarning($"Received a non-successful HTTP status code for domain {domainName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while checking domain availability for {domainName}");
                throw; 
            }

            return null;
        }

        public async Task AddDomainToFavoritesAsync(string domainName, bool? isAvailable, DateTime? expiryDate)
        {
            try
            {
                var existingDomain = await _repository.GetDomainByNameAsync(domainName);
                if (existingDomain != null)
                {
                    existingDomain.IsFavorite = true;
                    existingDomain.IsAvailable = isAvailable;
                    existingDomain.ExpiryDate = expiryDate;
                    existingDomain.LastChecked = DateTime.UtcNow;
                }
                else
                {
                    existingDomain = new DomainDto
                    {
                        Name = domainName,
                        IsAvailable = isAvailable,
                        LastChecked = DateTime.UtcNow,
                        ExpiryDate = expiryDate,
                        IsFavorite = true
                    };
                }
                await _repository.AddOrUpdateDomainAsync(existingDomain);
                _logger.LogInformation($"Added/updated domain {domainName} in favorites.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error adding domain {domainName} to favorites.");
                throw; 
            }
        }

        public async Task RemoveDomainFromFavoritesAsync(int domainId)
        {
            try
            {
                await _repository.RemoveDomainFromFavoritesAsync(domainId);
                _logger.LogInformation($"Removed domain with ID {domainId} from favorites.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing domain with ID {domainId} from favorites.");
                throw;
            }
        }

        public async Task<IEnumerable<DomainDto>> GetFavoriteDomainsAsync()
        {
            try
            {
                return await _repository.GetFavoriteDomainsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving favorite domains.");
                throw;
            }
        }
    }
}
