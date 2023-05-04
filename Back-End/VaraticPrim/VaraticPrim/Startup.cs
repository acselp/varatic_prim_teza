using System.Text;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VaraticPrim.Framework;
using VaraticPrim.JwtAuth;
using VaraticPrim.MvcExtentions;
using VaraticPrim.Repository.Persistence;
using VaraticPrim.Repository.Repository;
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
        services.AddControllers();
        services.AddOptions();
        services.AddEndpointsApiExplorer();
        services.AddFramework();
        services.AddServices();
        services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
        {
            builder.WithOrigins("http://localhost:5001")
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));
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

        //app.UseHangfireDashboard();
    }
}