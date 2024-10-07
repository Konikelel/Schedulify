using Dapper;
using Schedulify.App.Attributes;
using Schedulify.App.Database;
using Schedulify.App.Dtos;
using Schedulify.App.Entities;
using Schedulify.App.Enums;

namespace Schedulify.App.Repositories;

[Injectable(InjectableTypeEnum.Singleton, typeof(ScheduleRepository))]
public interface IScheduleRepository
{
    public Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<ScheduleEntity?> GetByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<IEnumerable<ScheduleEntity>> GetByCalendarIdAsync(Guid id, CancellationToken token = default);
    
    public Task<IEnumerable<ScheduleEntity>> GetByCategoryIdAsync(Guid id, CancellationToken token = default);
    
    public Task<IEnumerable<ScheduleEntity>> GetByOwnerIdAsync(Guid id, CancellationToken token = default);
    
    public Task<bool> CreateAsync(CreateScheduleDto scheduleDto, CancellationToken token = default);
    
    public Task<bool> UpdateAsync(UpdateScheduleDto scheduleDto, CancellationToken token = default);
    
    public Task<bool> UpdateCategoryIdAsync(Guid id, Guid newId, CancellationToken token = default);
    
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<bool> DeleteByCalendarIdAsync(Guid id, CancellationToken token = default);
}

public class ScheduleRepository : IScheduleRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    
    public ScheduleRepository(IDbConnectionFactory dbConnectionFactory)
    {
        this._dbConnectionFactory = dbConnectionFactory;
    }
    
    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        
        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(
            "SELECT COUNT(1) FROM Categories WHERE Id = @Id",
            new { Id = id },
            cancellationToken: token
        ));
    }

    public async Task<ScheduleEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        
        return await connection.QuerySingleOrDefaultAsync<ScheduleEntity>(new CommandDefinition(
            "SELECT * FROM Categories WHERE Id = @Id",
            new { Id = id },
            cancellationToken: token
        ));
    }

    public async Task<IEnumerable<ScheduleEntity>> GetByCalendarIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        
        return await connection.QueryAsync<ScheduleEntity>(new CommandDefinition(
            "SELECT * FROM Categories WHERE CalendarId = @Id",
            new { Id = id },
            cancellationToken: token
        ));
    }
    
    public async Task<IEnumerable<ScheduleEntity>> GetByCategoryIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        
        return await connection.QueryAsync<ScheduleEntity>(new CommandDefinition(
            "SELECT * FROM Categories WHERE CategoryId = @Id",
            new { Id = id },
            cancellationToken: token
        ));
    }
    
    public async Task<IEnumerable<ScheduleEntity>> GetByOwnerIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        
        return await connection.QueryAsync<ScheduleEntity>(new CommandDefinition(
            "SELECT * FROM Categories WHERE OwnerId = @Id",
            new { Id = id },
            cancellationToken: token
        ));
    }

    public async Task<bool> CreateAsync(CreateScheduleDto scheduleDto, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        
        var result = await connection.ExecuteAsync(new CommandDefinition(
            """
            INSERT INTO Schedules (
            Id, CalendarId, CategoryId, TimeStart, TimeEnd, Frequency, Title, Description, Link, OwnerId, UpdatedAt, CreatedAt
            ) VALUES (
            @Id, @CalendarId, @CategoryId, @TimeStart, @TimeEnd, @Frequency, @Title,  @Description, @Link, @OwnerId, @UpdatedAt, @CreatedAt)
            """,
            scheduleDto,
            transaction: transaction,
            cancellationToken: token
        ));
        
        return result > 0;
    }

    public async Task<bool> UpdateAsync(UpdateScheduleDto scheduleDto, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        
        var result = await connection.ExecuteAsync(new CommandDefinition(
            """
            UPDATE Schedules SET 
            CalendarId = @CalendarId, CategoryId = @CategoryId, TimeStart = @TimeStart, TimeEnd = @TimeEnd, Frequency = @Frequency, 
            Title = @Title, Description = @Description, Link = @Link, OwnerId = @OwnerId, UpdatedAt = @UpdatedAt
            WHERE Id = @Id
            """,
            scheduleDto,
            transaction: transaction,
            cancellationToken: token
        ));
        
        return result > 0;
    }
    
    public async Task<bool> UpdateCategoryIdAsync(Guid id, Guid newId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        
        var result = await connection.ExecuteAsync(new CommandDefinition(
            "UPDATE Schedules SET CategoryId = @NewId WHERE CategoryId = @Id",
            new { Id = id, NewId = newId },
            transaction: transaction,
            cancellationToken: token
        ));
        
        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        
        var result = await connection.ExecuteAsync(new CommandDefinition(
            "DELETE FROM Schedules WHERE Id = @Id",
            new { Id = id },
            transaction: transaction,
            cancellationToken: token
        ));
        
        return result > 0;
    }
    
    public async Task<bool> DeleteByCalendarIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        
        var result = await connection.ExecuteAsync(new CommandDefinition(
            "DELETE FROM Schedules WHERE CalendarId = @Id",
            new { Id = id },
            transaction: transaction,
            cancellationToken: token
        ));
        
        return result > 0;
    }
}