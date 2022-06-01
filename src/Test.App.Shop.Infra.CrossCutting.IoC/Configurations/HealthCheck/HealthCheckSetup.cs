using System;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Test.App.Shop.Infra.CrossCutting.Environments.Configurations;

namespace Test.App.Shop.Infra.CrossCutting.IoC.Configurations.HealthCheck;

public static class HealthCheckSetup
{
    public static void AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        var hcBuilder = services.AddHealthChecks();

        hcBuilder.AddCheck("Self Check API", () => HealthCheckResult.Healthy("HealthCheck Working"));
        // ADD OTHER CHECKS HERE

        hcBuilder
            .AddCheck<RequiredSectionsHealthCheck<ApplicationConfiguration>>(nameof(ApplicationConfiguration))
            .AddCheck<RequiredSectionsHealthCheck<JwtSettings>>(nameof(JwtSettings))
            .AddCheck<RequiredSectionsHealthCheck<RabbitMqConfiguration>>(nameof(RabbitMqConfiguration));

        var applicationConfiguration = configuration.GetSection(nameof(ApplicationConfiguration)).Get<ApplicationConfiguration>();
        hcBuilder.AddSqlServer(applicationConfiguration.ConnectionString ?? string.Empty, name: "Database HealthCheck", timeout: TimeSpan.FromSeconds(30));

        var rabbitMqConfiguration = configuration.GetSection(nameof(RabbitMqConfiguration)).Get<RabbitMqConfiguration>();
        var rabbitMqConnectionString = $"amqp://{rabbitMqConfiguration.Username}:{rabbitMqConfiguration.Password}@{rabbitMqConfiguration.Host}:5672//";
        hcBuilder.AddRabbitMQ(rabbitMqConnectionString, name: "RabbitMq HealthCheck", timeout: TimeSpan.FromSeconds(30));
    }

    public static void MapHealthCheck(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHealthChecks("/hc", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
        {
            Predicate = r => r.Name.Contains("self")
        });
    }
}
