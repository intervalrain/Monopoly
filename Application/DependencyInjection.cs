using System.Reflection;
using Application.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddMonopolyApplication(this IServiceCollection services)
    {
        services.AddUsecases();

        return services;
    }

    private static IServiceCollection AddUsecases(this IServiceCollection services)
    {
        Assembly assembly = typeof(DependencyInjection).Assembly;
        Type[] types = assembly.GetTypes();
        Type usecaseType = typeof(Usecase<>);

        foreach (var type in types)
        {
            if (type.BaseType?.IsGenericType == true && type.BaseType?.GetGenericTypeDefinition() == usecaseType)
            {
                services.AddTransient(type, type);
            }
        }
        return services;
    }
}