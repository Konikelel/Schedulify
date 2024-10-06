using System.Reflection;
using AutoMapper;
using Schedulify.App;

namespace Schedulify.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddSingleton<IMapper>(_ => CreateMapper());
        return services;
    }
    
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
            cfg.AddMaps(Assembly.GetAssembly(typeof(IAppMarker)))
        );
        return config.CreateMapper();
    }
}