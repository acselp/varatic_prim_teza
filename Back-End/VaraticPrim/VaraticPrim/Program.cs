using System.Net;
using VaraticPrim;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(builder.Configuration));
    
builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.Listen(IPAddress.Any, 5000);
});

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services); // calling ConfigureServices method 

var app = builder.Build();

startup.Configure(app, builder.Environment); // calling Configure method
app.MapControllers();

app.Run();