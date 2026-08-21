using LumiaFoundation.Logger.Extensions;
using LumiaFoundation.Logger.LoggerService;
using Microsoft.AspNetCore.HttpOverrides;
using Webapi.Extensions;
using Repository.Extensions;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddEnvironmentVariables()
    .Build();

builder.Services.ConfigureWebapiDatabase(configuration);
builder.Services.ConfigureRepositoryManager();

LoggerManager.LoadConfigurationFromFile(
    Path.Combine(builder.Environment.ContentRootPath, "nlog.config"));

// Add services to the container.
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureCors();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage(); else app.UseHsts();

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();
