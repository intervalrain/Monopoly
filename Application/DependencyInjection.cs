using Application.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddUseCases();
		return services;
	}

	private static IServiceCollection AddUseCases(this IServiceCollection services)
	{
		var assembly = typeof(DependencyInjection).Assembly;
		var types = assembly.GetTypes();
		var useCaseType = typeof(Usecase<>);

		foreach (var type in types)
		{
			if (type.BaseType?.IsGenericType == true &&
				type.BaseType?.GetGenericTypeDefinition() == useCaseType)
			{
				services.AddTransient(type, type);
			}
		}
		return services;
	}
}
