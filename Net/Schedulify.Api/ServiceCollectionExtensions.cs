using System.Reflection;
using AutoMapper;

namespace Schedulify.Apis;

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
            cfg.AddMaps(Assembly.GetExecutingAssembly())
        );
        return config.CreateMapper();
    }
}