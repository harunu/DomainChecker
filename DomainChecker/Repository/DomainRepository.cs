using DomainChecker.Data;
using DomainChecker.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DomainChecker.Repository
{
    public class DomainRepository : IDomainRepository
    {
        private readonly DomainContext _context;

        public DomainRepository(DomainContext context)
        {
            _context = context;
        }
        public async Task<DomainDto?> GetDomainByNameAsync(string domainName)
        {
            var domain = await _context.Domains
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(d => d.Name == domainName);

            if (domain == null)
            {
                Console.WriteLine($"A domain with the name {domainName} was not found.");
                return null;
            }

            return domain;
        }

        public async Task<IEnumerable<DomainDto>> GetAllDomainsAsync()
        {
            return await _context.Domains
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task AddOrUpdateDomainAsync(DomainDto domain)
        {
            var existingDomain = await _context.Domains
                                                .FirstOrDefaultAsync(d => d.Name == domain.Name);

            if (existingDomain != null)
            {
                existingDomain.IsAvailable = domain.IsAvailable;
                existingDomain.LastChecked = DateTime.UtcNow;
                existingDomain.ExpiryDate = domain.ExpiryDate;
                existingDomain.IsFavorite = domain.IsFavorite;
            }
            else
            {
                _context.Domains.Add(domain);
            }
            await _context.SaveChangesAsync();
        }

        public async Task RemoveDomainFromFavoritesAsync(int domainId)
        {
            var domain = await _context.Domains.FindAsync(domainId);
            if (domain != null)
            {
                domain.IsFavorite = false;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<DomainDto>> GetFavoriteDomainsAsync()
        {
            return await _context.Domains
                                 .Where(d => d.IsFavorite)
                                 .AsNoTracking()
                                 .ToListAsync();
        }
    }
}
