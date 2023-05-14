using Hangfire;
using Infrastructure.Migrations.Evolve;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using VaraticPrim.Background.Hangfire;
using VaraticPrim.Email;
using VaraticPrim.Framework;
using VaraticPrim.JwtAuth;
using VaraticPrim.MvcExtentions;
using VaraticPrim.Repository.Persistence;
using VaraticPrim.Repository.Repository;
using VaraticPrim.Repository.Repository.Implementations;
using VaraticPrim.Repository.Repository.Interfaces;
using VaraticPrim.Service;

namespace VaraticPrim;

public class Startup {
    
    private IConfiguration Config { get; }

    public Startup(IConfiguration configuration) 
    {
        Config = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        services.Configure<JwtConfiguration>(Config.GetSection("Jwt"));
        services.Configure<EmailOptions>(Config.GetSection("Email"));

        services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add<InternalServerErrorExceptionFilter>();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
        
        services.AddJwt(Config);
        services.AddDbContextPool<ApplicationDbContext>(options => options
            .UseLazyLoadingProxies()
            .UseNpgsql(Config.GetConnectionString("DefaultConnection"))
            .UseSnakeCaseNamingConvention());
        
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ICounterRepository, CounterRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        
        services.AddControllers();
        services.AddOptions();
        services.AddEndpointsApiExplorer();
        services.AddFramework();
        services.AddServices();
        
        services.AddMigrations(Config.GetConnectionString("DefaultConnection"));
        services.AddBackgroundJobs(Config.GetConnectionString("DefaultConnection"));
        
        services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));

        services.AddMailing();
        services.AddGmail();
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
        
        app.UseCors("MyPolicy");

        app.UseHangfireDashboard();
    }
}