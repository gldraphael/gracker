using Gracker.ServiceShell.Exceptions;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Gracker.ServiceShell;

public static class ExtensionsForMassTransit
{
    const string MESSAGING_CONFIG_KEY = "Messaging";

    public static void SetupGrackerService(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration.GetSection(MESSAGING_CONFIG_KEY).Get<MessagingConfig>();
        if (config.IsValid is false) throw new InvalidMessagingConfigurationException();

        var isTestEnvironment = builder.Environment.IsEnvironment("Test");

        builder.Services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            var entryAssembly = Assembly.GetEntryAssembly();
            x.AddConsumers(entryAssembly);

            if(isTestEnvironment)
            {
                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            }
            else
            {
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
            }
        });
    }
}
