using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Schedulify.App.Attributes;
using Schedulify.App.Database;
using Schedulify.App.Enums;

namespace Schedulify.App;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        foreach (var type in Assembly.GetAssembly(typeof(IAppMarker))!.GetTypes().Where(x => x.IsInterface))
        {
            var attribute = type.GetCustomAttribute(typeof(InjectableAttribute), false);
            
            if (attribute is InjectableAttribute injectableAttr)
            {
                if (!injectableAttr.Implementation.IsClass)
                {
                    throw new ArgumentException($"Implementation ({injectableAttr.Implementation.Name}) in injectableAttribute must be a type of class");
                }
                if (!type.IsAssignableFrom(injectableAttr.Implementation))
                {
                    throw new ArgumentException($"{injectableAttr.Implementation.Name} cannot implement {type.Name}");
                }
                
                switch (injectableAttr.InjectableType)
                {
                    case InjectableTypeEnum.Transient:
                        services.AddTransient(type, injectableAttr.Implementation);
                        break;
                    
                    case InjectableTypeEnum.Scoped:
                        services.AddScoped(type, injectableAttr.Implementation);
                        break;
                    
                    case InjectableTypeEnum.Singleton:
                        services.AddSingleton(type, injectableAttr.Implementation);
                        break;
                    
                    default:
                        throw new Exception("InjectableType not in enum");
                }
            }
        }
        
        services.AddValidatorsFromAssemblyContaining<IAppMarker>(ServiceLifetime.Singleton);
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new MsSqlConnectionFactory(connectionString));
        return services;
    }
}