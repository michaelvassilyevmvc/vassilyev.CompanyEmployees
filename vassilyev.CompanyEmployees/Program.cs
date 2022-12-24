using Microsoft.AspNetCore.HttpOverrides;
using vassilyev.CompanyEmployees.Extensions;
using NLog;
using Contracts;
using CompanyEmployees.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(configFile: string.Concat(str0: Directory.GetCurrentDirectory(), "/nlog.config"));


// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers()
    .AddApplicationPart(typeof(CompanyEmployee.Presentation.AssemblyReference).Assembly);

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if(app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});


app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
