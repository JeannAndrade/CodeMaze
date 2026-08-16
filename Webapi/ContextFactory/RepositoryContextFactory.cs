using Microsoft.EntityFrameworkCore.Design;
using Repository.Context;
using Webapi.Extensions;

namespace Webapi.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<WebapiDbContext>
    {
        public WebapiDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddEnvironmentVariables()
            .Build();

            var services = new ServiceCollection();

            services.ConfigureWebapiDatabase(configuration);

            return services.BuildServiceProvider().GetRequiredService<WebapiDbContext>();
        }
    }
}
