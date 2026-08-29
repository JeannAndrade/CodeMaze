using LumiaFoundation.Logger.Extensions;
using LumiaFoundation.Logger.LoggerService;
using Microsoft.AspNetCore.HttpOverrides;
using Service.Extensions;
using Persistence.Extensions;
using LumiaFoundation.AspNetCore.Commons.Extensions;
using Application.Companies.Queries.GetCompanyList;
using LumiaFoundation.AspNetCore.ExceptionHandlers;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

// A config abaixo é fundamental para que o app consiga ler as variáveis de ambiente do sistema operacional, como por exemplo, os paramêtros do banco de dados.
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddEnvironmentVariables()
    .Build();

// Add services to the container
builder.Services.ConfigureDatabase(configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.AddServicesFromAssembly(typeof(GetCompaniesListQuery).Assembly);

LoggerManager.LoadConfigurationFromFile(
    Path.Combine(builder.Environment.ContentRootPath, "nlog.config"));

builder.Services.ConfigureLoggerService();
builder.Services.ConfigureCors();
builder.Services.AddControllers();
// A config abaixo permite que o modelstate não seja validado automaticamente, permitindo que o tratamento de erros seja feito manualmente.
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.AddExceptionHandler<DomainExceptionHandler>();
builder.Services.AddExceptionHandler<UnhandledExceptionHandler>();

var app = builder.Build();

// var logger = app.Services.GetRequiredService<ILoggerManager>();
// app.ConfigureExceptionHandler(logger);
app.UseExceptionHandler(opt => { });
app.UseHsts();
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();
