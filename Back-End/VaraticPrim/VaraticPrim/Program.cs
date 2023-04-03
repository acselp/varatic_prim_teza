using VaraticPrim;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services); // calling ConfigureServices method 

var app = builder.Build();

startup.Configure(app, builder.Environment); // calling Configure method
app.MapControllers();

app.Run();