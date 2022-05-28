using System;
using Test.App.Shop.Application.Behaviors;
using Test.App.Shop.Domain.Exceptions;
using Test.App.Shop.Infra.CrossCutting.Environments.Configurations;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.App.Shop.Application.Adapters.Identity;
using Test.App.Shop.Domain.Aggregates.UserAggregate;
using Test.App.Shop.Domain.SeedWork;
using Test.App.Shop.Infra.CrossCutting.Identity.Core;
using Test.App.Shop.Infra.Data.Repositories.UserRepository;
using Test.App.Shop.Infra.Data.UnitOfWork;

namespace Test.App.Shop.Infra.CrossCutting.IoC;

public static class NativeInjectorBootstrapper
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        RegisterData(services);
        RegisterMediatR(services);
        RegisterIdentity(services);
        RegisterEnvironments(services, configuration);
    }

    private static void RegisterData(IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    private static void RegisterMediatR(IServiceCollection services)
    {
        const string applicationAssemblyName = "Test.App.Shop.Application"; // use your project name
        var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

        AssemblyScanner
            .FindValidatorsInAssembly(assembly)
            .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

        // injection for Mediator
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PipelineBehavior<,>));
        services.AddScoped<INotificationHandler<ExceptionNotification>, ExceptionNotificationHandler>();
    }

    private static void RegisterEnvironments(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration.GetSection(nameof(ApplicationConfiguration)).Get<ApplicationConfiguration>());
        services.AddSingleton(configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>());
    }

    private static void RegisterIdentity(IServiceCollection services)
    {
        services.AddScoped<ITokenManager, JwtTokenManager>();
    }
}
