using System.Data;
using Dapper;

namespace Schedulify.App;

public static class DapperExtensions
{
    public static async Task<int> ExecuteAsyncTransaction(this IDbConnection cnn, CommandDefinition command, bool affectMultple = false)
    {
        var result = await cnn.ExecuteAsync(command);

        if (result <= 1 || affectMultple)
        {
            command.Transaction?.Commit();
            return result;
        }
        
        command.Transaction?.Rollback();
        return 0;
    }
}