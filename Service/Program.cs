using LumiaFoundation.Logger.Extensions;
using LumiaFoundation.Logger.LoggerService;
using Microsoft.AspNetCore.HttpOverrides;
using Service.Extensions;
using Persistence.Extensions;
using LumiaFoundation.AspNetCore.Commons.Extensions;
using Application.Companies.Queries.GetCompanyList;
using LumiaFoundation.AspNetCore.ExceptionHandlers;
using Microsoft.AspNetCore.Mvc;
using LumiaFoundation.Auth.Extensions;
using LumiaFoundation.AspNetCore.Extensions;
using LumiaFoundation.Auth.Config;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddEnvironmentVariables()
    .Build();

# region Configure Domain Database
builder.Services.ConfigureDatabase(configuration);
builder.Services.ConfigureRepositoryManager();
#endregion

builder.Services.AddValidationFilters();

#region Authentication and Authorization
builder.Services.ConfigureIdentity();
builder.Services.ConfigureIdentityDatabase(configuration);
builder.Services.ConfigureIdentityServiceManager();
var appConfigurationParameter = new AppConfigurationParameter(configuration);
builder.Services.ConfigureAppSettingsReader(appConfigurationParameter);
builder.Services.ConfigureJWT(appConfigurationParameter);
builder.Services.AddAuthentication();
#endregion

#region Serviços da camada de aplicação
builder.Services.AddServicesFromAssembly(typeof(GetCompaniesListQuery).Assembly);
#endregion

#region Configurando logs
LoggerManager.LoadConfigurationFromFile(
    Path.Combine(builder.Environment.ContentRootPath, "nlog.config"));
builder.Services.ConfigureLoggerService();
#endregion

builder.Services.ConfigureCors();
builder.Services.AddControllers();

#region Configurando comportamento tratamento de exceções
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.AddDomainExceptionMappingFilter();
builder.Services.AddExceptionHandler<DomainExceptionHandler>();
builder.Services.AddExceptionHandler<UnhandledExceptionHandler>();
#endregion

#region Configurando OpenAPI
builder.Services.ConfigureOpenApi("CompanyEmployees API", "v1");
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new()
        {
            Title = "CompanyEmployees API",
            Version = "v1"
        };
        return Task.CompletedTask;
    });
});
#endregion

var app = builder.Build();

app.UseExceptionHandler(opt => { });
app.UseHsts();
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });
app.UseCors("CorsPolicy");
// Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapOpenApiDevTools();
app.MapControllers();

app.Run();