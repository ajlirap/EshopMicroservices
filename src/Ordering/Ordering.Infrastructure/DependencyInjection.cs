﻿using Microsoft.EntityFrameworkCore.Diagnostics;
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

        services.AddScoped<ISaveChangesInterceptor, SaveChangesInterceptor>();
        services.AddScoped<ISaveChangesInterceptor ,DispatchDomainEventsInterceptor>();


        services.AddDbContext<ApplicationDbContext>((sp, options) => 
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString).EnableSensitiveDataLogging();
        });
    
        //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}
