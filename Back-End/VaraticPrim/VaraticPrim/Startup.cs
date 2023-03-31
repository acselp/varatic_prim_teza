namespace VaraticPrim;

public class Startup {
    public IConfiguration configRoot { get; }
    
    public Startup(IConfiguration configuration) 
    {
        configRoot = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services) 
    {
        services.AddRazorPages();
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
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}