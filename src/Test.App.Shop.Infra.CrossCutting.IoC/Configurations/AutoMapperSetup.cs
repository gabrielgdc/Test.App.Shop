using System;
using Test.App.Shop.Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Test.App.Shop.Infra.CrossCutting.IoC.Configurations;

public static class AutoMapperSetup
{
	public static void AddAutoMapper(this IServiceCollection services)
	{
		if (services == null) throw new ArgumentNullException(nameof(services));

		services.AddAutoMapper(typeof(MappingProfile));
	}
}