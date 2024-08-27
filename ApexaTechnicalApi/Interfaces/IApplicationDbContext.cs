using ApexaTechnicalApi.Models;
using Microsoft.EntityFrameworkCore;


public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
