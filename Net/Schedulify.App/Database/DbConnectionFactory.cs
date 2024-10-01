using System.Data;
using Microsoft.Data.SqlClient;

namespace Schedulify.App.Database;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default);
}

public class MsSqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public MsSqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default)
    {
        var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(token);
        return connection;
    }
}
