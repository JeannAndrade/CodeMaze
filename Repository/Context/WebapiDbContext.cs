using Entities.Models;
using LumiaFoundation.EFRepository.Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Mappings;

namespace Repository.Context
{
  public class WebapiDbContext(DbContextOptions options) : RepositoryContext(options)
  {
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebapiDbContext).Assembly);

      base.OnModelCreating(modelBuilder);
    }

    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Employee> Employees => Set<Employee>();
  }
}
