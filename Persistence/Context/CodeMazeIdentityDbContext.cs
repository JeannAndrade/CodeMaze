using LumiaFoundation.Auth.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{

    public class CodeMazeIdentityDbContext(DbContextOptions<CodeMazeIdentityDbContext> options) : IdentityContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
