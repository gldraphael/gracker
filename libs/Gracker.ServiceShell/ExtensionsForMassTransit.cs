using Gracker.ServiceShell.Exceptions;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Gracker.ServiceShell;

public static class ExtensionsForMassTransit
{
    const string MESSAGING_CONFIG_KEY = "Messaging";

    public static void SetupGrackerService(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration.GetSection(MESSAGING_CONFIG_KEY).Get<MessagingConfig>();
        if (config.IsValid is false) throw new InvalidMessagingConfigurationException();



        builder.Services.AddMassTransit(x =>
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

                cfg.ConfigureJsonSerializerOptions(opts =>
                {
                    opts.Converters.Add(new JsonIPAddressConverter());
                    opts.Converters.Add(new JsonIPEndPointConverter());
                    return opts;
                });
                cfg.ConfigureEndpoints(context);
            });
        });
    }
}
