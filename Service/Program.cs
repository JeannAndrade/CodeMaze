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

var appConfigurationParameter = new AppConfigurationParameter(configuration);

builder.Services.ConfigureDatabase(configuration);
builder.Services.ConfigureIdentityDatabase(configuration);
builder.Services.AddValidationFilters();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureIdentityServiceManager();
builder.Services.ConfigureAppSettingsReader(appConfigurationParameter);
builder.Services.ConfigureJWT(appConfigurationParameter);
builder.Services.ConfigureRepositoryManager();
builder.Services.AddServicesFromAssembly(typeof(GetCompaniesListQuery).Assembly);

LoggerManager.LoadConfigurationFromFile(
    Path.Combine(builder.Environment.ContentRootPath, "nlog.config"));

builder.Services.ConfigureLoggerService();
builder.Services.ConfigureCors();
builder.Services.AddControllers();
builder.Services.ConfigureOpenApi("CompanyEmployees API", "v1");
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.AddDomainExceptionMappingFilter();
builder.Services.AddExceptionHandler<DomainExceptionHandler>();
builder.Services.AddExceptionHandler<UnhandledExceptionHandler>();

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

var app = builder.Build();

app.UseExceptionHandler(opt => { });
app.UseHsts();
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapOpenApiDevTools();
app.MapControllers();

app.Run();