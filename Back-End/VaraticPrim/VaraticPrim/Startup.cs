using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using VaraticPrim.JwtAuth;
using VaraticPrim.MvcExtentions;
using VaraticPrim.Repository.Persistance;
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
        services.AddControllers();
        services.AddOptions();
        services.AddEndpointsApiExplorer();
        services.AddAutoMapper(typeof(Startup));
        services.AddServices();
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