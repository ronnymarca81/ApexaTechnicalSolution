using ApexaTechnicalApi.Models;
using Microsoft.EntityFrameworkCore;


public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Advisor> Advisors { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
