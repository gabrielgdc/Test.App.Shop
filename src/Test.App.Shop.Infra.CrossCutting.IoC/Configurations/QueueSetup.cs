using System;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.App.Shop.Application.Consumers;
using Test.App.Shop.Infra.CrossCutting.Environments.Configurations;

namespace Test.App.Shop.Infra.CrossCutting.IoC.Configurations;

public static class QueueSetup
{
    public static void AddQueue(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqConfiguration = configuration.GetSection(nameof(RabbitMqConfiguration)).Get<RabbitMqConfiguration>();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<SubmittedOrderConsumer>();

            x.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(rabbitMqConfiguration.Host, h =>
                {
                    h.Username(rabbitMqConfiguration.Username);
                    h.Password(rabbitMqConfiguration.Password);
                });

                configurator.ReceiveEndpoint(rabbitMqConfiguration.SubmittedOrderQueueName ?? string.Empty,
                    e =>
                    {
                        e.ConfigureConsumer<SubmittedOrderConsumer>(context,
                            consumerConfigurator =>
                            {
                                consumerConfigurator.UseMessageRetry(retry => { retry.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30)); });
                            });
                    });

                configurator.ConfigureEndpoints(context);
            });
        });
    }
}
