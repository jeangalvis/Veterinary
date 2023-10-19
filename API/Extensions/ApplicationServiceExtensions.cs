using Application.UnitOfWork;
using AspNetCoreRateLimit;
using Domain.Interfaces;

namespace API.Extensions;
public static class ApplicationServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
    services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()    //WithOrigins("https://domain.com")
                .AllowAnyMethod()       //WithMethods("GET","POST)
                .AllowAnyHeader());     //WithHeaders("accept","content-type")
    });
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
    public static void ConfigureRateLimiting(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddInMemoryRateLimiting();
        services.Configure<IpRateLimitOptions>

            (options =>
            {
                options.EnableEndpointRateLimiting = true;
                options.StackBlockedRequests = false;
                options.HttpStatusCode = 429;
                options.RealIpHeader = "X-Real-Ip";
                options.GeneralRules = new List<RateLimitRule>
                {
                        new RateLimitRule{Endpoint="*",Period="10s",Limit=2}
                };
            }

        );

    }
}
