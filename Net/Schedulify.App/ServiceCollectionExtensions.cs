using Microsoft.Extensions.DependencyInjection;
using Schedulify.App.Database;

namespace Schedulify.App;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // services.AddSingleton<IMovieService, MovieService>();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new MsSqlConnectionFactory(connectionString));
        return services;
    }
}