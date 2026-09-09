using Domain.Models;
using LumiaFoundation.EFRepository.Repository;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
  public class CodeMazeDbContext(DbContextOptions options) : RepositoryContext(options)
  {
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(CodeMazeDbContext).Assembly);

      base.OnModelCreating(modelBuilder);
    }

    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Employee> Employees => Set<Employee>();
  }
}
