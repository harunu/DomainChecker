using DomainChecker.Data;

namespace DomainChecker.Interfaces
{
    public interface IDomainService
    {
        Task<DomainDto> CheckDomainAvailabilityAsync(string domainName);
        Task AddDomainToFavoritesAsync(string domainName, bool? isAvailable, DateTime? expiryDate);
        Task RemoveDomainFromFavoritesAsync(int domainId);
        Task<IEnumerable<DomainDto>> GetFavoriteDomainsAsync();
    }
}
