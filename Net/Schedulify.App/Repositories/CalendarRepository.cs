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
            "SELECT COUNT(1) FROM Calendars WHERE Id = @Id",
            new { Id = id },
            cancellationToken: token
        ));
    }
    

    public async Task<CalendarEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        return await connection.QuerySingleOrDefaultAsync<CalendarEntity>(new CommandDefinition(
            "SELECT * FROM Calendars WHERE Id = @Id",
            new { Id = id },
            cancellationToken: token
        ));
    }
    
    public async Task<IEnumerable<CalendarEntity>> GetByOwnerIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        return await connection.QueryAsync<CalendarEntity>(new CommandDefinition(
            "SELECT * FROM Calendars WHERE OwnerId = @Id",
            new { Id = id },
            cancellationToken: token
        ));
    }

    public async Task<bool> CreateAsync(CreateCalendarDto calendarDto, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        
        var result = await connection.ExecuteAsyncTransaction(new CommandDefinition(
            "INSERT INTO Calendars (Id, Name, OwnerId, CreatedAt, UpdatedAt) VALUES (@Id, @Name, @OwnerId, @CreatedAt, @UpdatedAt)",
            calendarDto,
            transaction: transaction,
            cancellationToken: token
        ));
        
        return result > 0;
    }

    public async Task<bool> UpdateAsync(UpdateCalendarDto calendarDto, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        
        var result = await connection.ExecuteAsyncTransaction(new CommandDefinition(
            "UPDATE Calendars SET Name = @Name, OwnerId = @OwnerId, UpdatedAt = @UpdatedAt WHERE Id = @Id",
            calendarDto,
            transaction: transaction,
            cancellationToken: token
        ));
        
        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        
        var result = await connection.ExecuteAsyncTransaction(new CommandDefinition(
            "DELETE FROM Calendars WHERE Id = @Id",
            new { Id = id },
            transaction: transaction,
            cancellationToken: token
        ));
        
        return result > 0;
    }
}