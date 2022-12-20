using MyFlow.Data;
using MyFlow.Service;
using EnvironmentName = Microsoft.AspNetCore.Hosting.EnvironmentName;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var isProd = environment == EnvironmentName.Development;
// var logger = NLogBuilder.ConfigureNLog(isProd ? "nlog.config" : "nlog.debug.config").GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Path.GetFullPath(Directory.GetCurrentDirectory()),
    WebRootPath = "wwwroot",
    Args = args
});

// builder.Host.ConfigureDefaults(args);
// builder.Host.ConfigureServices();
builder.Host.ConfigureAppConfiguration((hostContext, config) =>
{
    var env = hostContext.HostingEnvironment;
    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
});
// builder.Host.UseNLog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext(connString);
builder.Services.AddDAOs();
builder.Services.AddServices();
builder.Services.AddActions();
builder.Services.AddTag();
builder.Services.AddTarget();
builder.Services.AddTitle();
builder.Services.AddValidation();
builder.Services.AddDeadline();
builder.Services.AddSwitch();
builder.Services.AddJob();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
