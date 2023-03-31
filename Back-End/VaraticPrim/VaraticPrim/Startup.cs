namespace VaraticPrim;

public class Startup {
    public IConfiguration ConfigRoot { get; }
    
    public Startup(IConfiguration configuration) 
    {
        ConfigRoot = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddOptions();
        services.AddEndpointsApiExplorer();
    }
    
    public void Configure(WebApplication app, IWebHostEnvironment env) 
    {
        if (!app.Environment.IsDevelopment()) 
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