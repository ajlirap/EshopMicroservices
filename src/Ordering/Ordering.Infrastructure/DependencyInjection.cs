using Microsoft.Extensions.Configuration;
using Ordering.Infrastructure.Data.Interceptors;
namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        //Add Services to the container
        services.AddDbContext<ApplicationDbContext>(options => 
        {
            options.AddInterceptors(new AuditableEntityInterceptor());
            options.UseSqlServer(connectionString).EnableSensitiveDataLogging();
        });
    
        //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}
