using System.Data;
using Dapper;

namespace Schedulify.App;

public static class DapperExtensions
{
    public static async Task<int> ExecuteAsyncWithTransaction(
        this IDbConnection cnn,
        string sqlQuery,
        object? parameters = null,
        bool affectMultiple = false,
        CancellationToken cancellationToken = default)
    {
        using var transaction = cnn.BeginTransaction();
        var result = 0;

        try
        {
            result = await cnn.ExecuteAsync(
                new CommandDefinition(sqlQuery, parameters, transaction: transaction, cancellationToken: cancellationToken)
            );
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
        
        if (result == 0 || (result > 1 && !affectMultiple)) transaction.Rollback(); else transaction.Commit();

        return result;
    }
}