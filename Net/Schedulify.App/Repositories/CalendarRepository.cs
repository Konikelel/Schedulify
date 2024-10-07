using System.Data;
using Dapper;
using Schedulify.App.Attributes;
using Schedulify.App.Database;
using Schedulify.App.Dtos;
using Schedulify.App.Entities;
using Schedulify.App.Enums;

namespace Schedulify.App.Repositories;

[Injectable(InjectableTypeEnum.Singleton, typeof(CalendarRepository))]
public interface ICalendarRepository
{
    public Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<CalendarEntity?> GetByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<IEnumerable<CalendarEntity>> GetByOwnerIdAsync(Guid id, CancellationToken token = default);
    
    public Task<bool> CreateAsync(CreateCalendarDto calendarDto, CancellationToken token = default);
    
    public Task<bool> UpdateAsync(UpdateCalendarDto calendarDto, CancellationToken token = default);
    
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
}

public class CalendarRepository : ICalendarRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    
    public CalendarRepository(IDbConnectionFactory dbConnectionFactory)
    {
        this._dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(
            "EXEC dbo.spCalendarsCount",
            new { Id = id },
            cancellationToken: token,
            commandType: CommandType.StoredProcedure
        ));
    }
    

    public async Task<CalendarEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        return await connection.QuerySingleOrDefaultAsync<CalendarEntity>(new CommandDefinition("dbo.spCalendarsGet",
            new { Id = id, ReturnFirst = 1 },
            cancellationToken: token,
            commandType: CommandType.StoredProcedure
        ));
    }
    
    public async Task<IEnumerable<CalendarEntity>> GetByOwnerIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        return await connection.QueryAsync<CalendarEntity>(new CommandDefinition("dbo.spCalendarsGet",
            new { OwnerId = id, ReturnFirst = 1 },
            cancellationToken: token,
            commandType: CommandType.StoredProcedure
        ));
    }

    public async Task<bool> CreateAsync(CreateCalendarDto calendarDto, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        
        var result = await connection.ExecuteAsyncTransaction(new CommandDefinition("dbo.spCalendarsInsert",
            calendarDto,
            transaction: transaction,
            cancellationToken: token,
            commandType: CommandType.StoredProcedure
        ));
        
        return result > 0;
    }

    public async Task<bool> UpdateAsync(UpdateCalendarDto calendarDto, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        
        var result = await connection.ExecuteAsyncTransaction(new CommandDefinition("dbo.spCalendarsUpdate",
            calendarDto,
            transaction: transaction,
            cancellationToken: token,
            commandType: CommandType.StoredProcedure
        ));
        
        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        
        var result = await connection.ExecuteAsyncTransaction(new CommandDefinition("dbo.spCalendarsDelete",
            new { Id = id },
            transaction: transaction,
            cancellationToken: token,
            commandType: CommandType.StoredProcedure
        ));
        
        return result > 0;
    }
}