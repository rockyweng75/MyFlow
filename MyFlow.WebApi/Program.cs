using MyFlow.Data;
using MyFlow.Service;
using MyFlow.WebApi.Controllers;
using MyFlow.WebApi.Security;
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
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(2)
};

app.UseWebSockets(webSocketOptions);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
