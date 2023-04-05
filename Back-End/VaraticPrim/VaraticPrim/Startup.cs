using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using VaraticPrim.AutoMapperProfiles;
using VaraticPrim.Repository;
using VaraticPrim.Repository.Persistance;
using VaraticPrim.Repository.Repository;

namespace VaraticPrim;

public class Startup {
    
    private IConfiguration _config { get; }

    public Startup(IConfiguration configuration) 
    {
        _config = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContextPool<ApplicationDbContext>(options => options
            .UseNpgsql(_config.GetConnectionString("DefaultConnection"))
            .UseSnakeCaseNamingConvention());

        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddControllers();
        services.AddOptions();
        services.AddEndpointsApiExplorer();
        services.AddAutoMapper(typeof(Startup));
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
        app.UseAuthentication();
    }
}