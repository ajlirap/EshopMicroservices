using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EshopMicro.Messaging.MassTransit;

public static class Extensions
{

    public static IServiceCollection AddMessageBroker(this IServiceCollection services, Assembly? assembly = null)
    {
        //Implemet RabbitMQ MassTransit configuration
        return services;
    }
}
