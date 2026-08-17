using LumiaFoundation.EFRepository.Extensions;
using LumiaFoundation.EFRepository.Utils;
using Repository.Context;
using Webapi.Utils;

namespace Webapi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });

        public static IServiceCollection ConfigureWebapiDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var host = configuration["DBHOST"] ?? "localhost";
            var port = configuration["DBPORT"] ?? "3306";
            var user = configuration["CODEMAZE_DB_USER"] ?? "example-user";
            var password = configuration["CODEMAZE_DB_PASS"] ?? "my_cool_secret";
            var database = configuration["CODEMAZE_DB_NAME"] ?? "my_database";
            var majorVersion = Utilitarios.ParaIntOuPadrao(configuration["DBMAJORVERSION"] ?? "", 9);
            var minorVersion = Utilitarios.ParaIntOuPadrao(configuration["DBMINORVERSION"] ?? "", 7);
            var buildVersion = Utilitarios.ParaIntOuPadrao(configuration["DBBUILDVERSION"] ?? "", 2);

            var dbConnectionHelper = new DbConnectionHelper(host: host, port: port, user: user, password: password, database: database, majorVersion: majorVersion, minorVersion: minorVersion, buildVersion: buildVersion);

            services.ConfigureMySqlDbDatabase<WebapiDbContext>(dbConnectionHelper, "Webapi");

            return services;
        }
    }
}
