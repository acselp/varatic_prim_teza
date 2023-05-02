using Hangfire;
using Hangfire.Dashboard.Resources;
using Hangfire.PostgreSql;
using Infrastructure.Migrations.Evolve;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using VaraticPrim.Background.Hangfire;
using VaraticPrim.Framework;
using VaraticPrim.JwtAuth;
using VaraticPrim.MvcExtentions;
using VaraticPrim.Repository.Persistence;
using VaraticPrim.Repository.Repository;
using VaraticPrim.Service;

namespace VaraticPrim;

public class Startup {
    
    private IConfiguration _config { get; }

    public Startup(IConfiguration configuration) 
    {
        _config = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.Configure<JwtConfiguration>(_config.GetSection("Jwt"));

        services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add<InternalServerErrorExceptionFilter>();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
        
        services.AddJwt(_config);
        services.AddDbContextPool<ApplicationDbContext>(options => options
            .UseLazyLoadingProxies()
            .UseNpgsql(_config.GetConnectionString("DefaultConnection"))
            .UseSnakeCaseNamingConvention());
        
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ICounterRepository, CounterRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddControllers();
        services.AddOptions();
        services.AddEndpointsApiExplorer();
        services.AddFramework();
        services.AddServices();
        services.AddMigrations(_config.GetConnectionString("DefaultConnection"));
        services.AddBackgroundJobs(_config.GetConnectionString("DefaultConnection"));
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
        
        app.UseAuthorization();
        
        app.UseHangfireDashboard();
    }
}