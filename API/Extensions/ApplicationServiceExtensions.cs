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
}
