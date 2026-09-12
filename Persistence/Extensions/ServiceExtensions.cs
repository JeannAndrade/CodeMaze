using LumiaFoundation.Auth.Persistence;
using LumiaFoundation.Core.Utils;
using LumiaFoundation.EFRepository.Extensions;
using LumiaFoundation.EFRepository.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Management;

namespace Persistence.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration) =>
            services.ConfigureMySqlContext<CodeMazeDbContext>(configuration);

        public static IServiceCollection ConfigureIdentityDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureMySqlContext<CodeMazeIdentityDbContext>(configuration);
            services.AddScoped<IdentityContext>(provider =>
                provider.GetRequiredService<CodeMazeIdentityDbContext>());

            return services;
        }

        private static IServiceCollection ConfigureMySqlContext<TContext>(
            this IServiceCollection services,
            IConfiguration configuration)
            where TContext : DbContext
        {
            var dbConnectionHelper = BuildDbConnectionHelper(configuration);
            services.ConfigureMySqlDbDatabase<TContext>(dbConnectionHelper, "Persistence");
            return services;
        }

        private static DbConnectionHelper BuildDbConnectionHelper(IConfiguration configuration) =>
            new(
                host: configuration["DBHOST"] ?? "localhost",
                port: configuration["DBPORT"] ?? "3306",
                user: configuration["CODEMAZE_DB_USER"] ?? "example-user",
                password: configuration["CODEMAZE_DB_PASS"] ?? "my_cool_secret",
                database: configuration["CODEMAZE_DB_NAME"] ?? "my_database",
                majorVersion: ParseHelper.ToIntOrDefault(configuration["DBMAJORVERSION"] ?? "", 9),
                minorVersion: ParseHelper.ToIntOrDefault(configuration["DBMINORVERSION"] ?? "", 7),
                buildVersion: ParseHelper.ToIntOrDefault(configuration["DBBUILDVERSION"] ?? "", 2)
            );
    }
}