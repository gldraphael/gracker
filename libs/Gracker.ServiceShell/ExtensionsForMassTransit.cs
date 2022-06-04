using Gracker.ServiceShell.Exceptions;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Gracker.ServiceShell;

public static class ExtensionsForMassTransit
{
    const string MESSAGING_CONFIG_KEY = "Messaging";

    public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var config = configuration.GetSection(MESSAGING_CONFIG_KEY).Get<MessagingConfig>();
        if (config.IsValid is false) throw new InvalidMessagingConfigurationException();

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            var entryAssembly = Assembly.GetEntryAssembly();
            x.AddConsumers(entryAssembly);

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(
                    host: config.Host,
                    port: config.Port,
                    virtualHost: config.VirtualHost,
                    h => {
                        h.Username(config.Username);
                        h.Password(config.Password);
                    });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
