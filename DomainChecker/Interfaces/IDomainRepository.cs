using DomainChecker.Data;

namespace DomainChecker.Interfaces
{
    public interface IDomainRepository
    {
        Task<DomainDto?> GetDomainByNameAsync(string domainName);
        Task<IEnumerable<DomainDto>> GetAllDomainsAsync();
        Task AddOrUpdateDomainAsync(DomainDto domain);
        Task RemoveDomainFromFavoritesAsync(int domainId);
        Task<IEnumerable<DomainDto>> GetFavoriteDomainsAsync();
    }
}
