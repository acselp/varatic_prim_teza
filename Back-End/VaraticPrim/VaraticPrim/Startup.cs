using System.Security.AccessControl;
using System.Text;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using VaraticPrim.AutoMapperProfiles;
using VaraticPrim.Repository;
using VaraticPrim.Repository.Persistance;
using VaraticPrim.Repository.Repository;
using VaraticPrim.Validation;

namespace VaraticPrim;

public class Startup {
    
    private IConfiguration _config { get; }

    public Startup(IConfiguration configuration) 
    {
        _config = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateTokenReplay = true,
                    ValidIssuer = _config.GetSection("Jwt").GetSection("Issuer").Value,
                    ValidAudience = _config.GetSection("Jwt").GetSection("Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            _config
                                .GetSection("Jwt")
                                .GetSection("Key")
                                .Value))
                };
            });
        services.AddDbContextPool<ApplicationDbContext>(options => options
            .UseLazyLoadingProxies()
            .UseNpgsql(_config.GetConnectionString("DefaultConnection"))
            .UseSnakeCaseNamingConvention());
        
        services.AddValidatorsFromAssembly(GetType().Assembly);
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
        app.UseAuthorization();
    }
}