using DomainChecker.Data;
using Microsoft.EntityFrameworkCore;

public class DomainContext : DbContext
{
    public DomainContext(DbContextOptions<DomainContext> options) : base(options) { }

    public DbSet<DomainDto> Domains { get; set; }
}
