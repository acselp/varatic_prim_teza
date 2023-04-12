using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using VaraticPrim.MvcExtentions.Errors;

namespace VaraticPrim.JwtAuth;

public static class JwtAuthConfigurator
{
    public static void AddJwt(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateTokenReplay = true,
                    ValidIssuer = configuration.GetSection("Jwt").GetSection("Issuer").Value,
                    ValidAudience = configuration.GetSection("Jwt").GetSection("Audience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            configuration
                                .GetSection("Jwt")
                                .GetSection("Key")
                                .Value))
                };
                options.Events = ConfigureJwtEvents();
            });
    }
    
    private static JwtBearerEvents ConfigureJwtEvents()
    {
        var bearerEvents = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();
 
                var error = ApiErrorBuilder.New()
                    .SetMessage("Unauthorized")
                    .SetCode(ApiErrorCodes.Unauthorized)
                    .Build();
 
                context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                return context.Response.WriteAsync(JsonConvert.SerializeObject(error));
            },
            OnForbidden = context =>
            {
                var error = ApiErrorBuilder.New()
                    .SetMessage("Forbidden")
                    .SetCode(ApiErrorCodes.Forbidden)
                    .Build();
 
                context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                return context.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
        };
 
        return bearerEvents;
    }
}