using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VaraticPrim.Repository;
using VaraticPrim.Repository.Persistance;

namespace VaraticPrim;

public class Startup {
    
    private IConfiguration _config { get; }

    public Startup(IConfiguration configuration) 
    {
        _config = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContextPool<ApplicationDbContext>(
            options => options.UseNpgsql(_config.GetConnectionString("DefaultConnection")));
        
        services.AddControllers();
        services.AddOptions();
        services.AddEndpointsApiExplorer();
    }
    
    public void Configure(WebApplication app, IWebHostEnvironment env) 
    {
        if (!env.IsDevelopment()) 
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseAuthentication();
    }
}