using System.Data;
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
    
    public Task<IEnumerable<ScheduleEntity>> GetByCalendarIdAsync(Guid calendarId, CancellationToken token = default);
    
    public Task<IEnumerable<ScheduleEntity>> GetByCategoryIdAsync(Guid categoryId, CancellationToken token = default);
    
    public Task<IEnumerable<ScheduleEntity>> GetByOwnerIdAsync(Guid ownerId, CancellationToken token = default);
    
    public Task<bool> CreateAsync(CreateScheduleDto scheduleDto, CancellationToken token = default);
    
    public Task<bool> UpdateAsync(UpdateScheduleDto scheduleDto, CancellationToken token = default);
    
    public Task<bool> UpdateCategoryIdAsync(Guid oldCategoryId, Guid newCategoryId, CancellationToken token = default);
    
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<bool> DeleteByCalendarIdAsync(Guid calendarId, CancellationToken token = default);
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
        
        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition("dbo.spSchedulesCount",
            new { Id = id},
            cancellationToken: token,
            commandType: CommandType.StoredProcedure
        ));
    }

    public async Task<ScheduleEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        
        return await connection.QuerySingleOrDefaultAsync<ScheduleEntity>(new CommandDefinition("dbo.spSchedulesGet",
            new { Id = id, ReturnFirst = 1 },
            cancellationToken: token,
            commandType: CommandType.StoredProcedure
        ));
    }

    public async Task<IEnumerable<ScheduleEntity>> GetByCalendarIdAsync(Guid calendarId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        
        return await connection.QueryAsync<ScheduleEntity>(new CommandDefinition("dbo.spSchedulesGet",
            new { CalendarId = calendarId, ReturnFirst = 0 },
            cancellationToken: token,
            commandType: CommandType.StoredProcedure
        ));
    }
    
    public async Task<IEnumerable<ScheduleEntity>> GetByCategoryIdAsync(Guid categoryId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        
        return await connection.QueryAsync<ScheduleEntity>(new CommandDefinition("dbo.spSchedulesGet",
            new { CategoryId = categoryId, UseCategoryId = 1, ReturnFirst = 0 },
            cancellationToken: token,
            commandType: CommandType.StoredProcedure
        ));
    }
    
    public async Task<IEnumerable<ScheduleEntity>> GetByOwnerIdAsync(Guid ownerId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        
        return await connection.QueryAsync<ScheduleEntity>(new CommandDefinition("dbo.spSchedulesGet",
            new { OwnerId = ownerId, ReturnFirst = 0 },
            cancellationToken: token,
            commandType: CommandType.StoredProcedure
        ));
    }

    public async Task<bool> CreateAsync(CreateScheduleDto scheduleDto, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        
        var result = await connection.ExecuteAsync(new CommandDefinition("dbo.spSchedulesInsert",
            scheduleDto,
            transaction: transaction,
            cancellationToken: token,
            commandType: CommandType.StoredProcedure
        ));
        
        return result > 0;
    }

    public async Task<bool> UpdateAsync(UpdateScheduleDto scheduleDto, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        
        var result = await connection.ExecuteAsync(new CommandDefinition("dbo.spSchedulesUpdate",
            scheduleDto,
            transaction: transaction,
            cancellationToken: token,
            commandType: CommandType.StoredProcedure
        ));
        
        return result > 0;
    }
    
    public async Task<bool> UpdateCategoryIdAsync(Guid oldCategoryId, Guid newCategoryId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        
        var result = await connection.ExecuteAsync(new CommandDefinition("dbo.spSchedulesUpdateCategory",
            new { OldId = oldCategoryId, NewId = newCategoryId },
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
        
        var result = await connection.ExecuteAsync(new CommandDefinition("dbo.spSchedulesDelete",
            new { Id = id },
            transaction: transaction,
            cancellationToken: token,
            commandType: CommandType.StoredProcedure
        ));
        
        return result > 0;
    }
    
    public async Task<bool> DeleteByCalendarIdAsync(Guid calendarId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        
        var result = await connection.ExecuteAsync(new CommandDefinition("dbo.spSchedulesDelete",
            new { CalendarId = calendarId },
            transaction: transaction,
            cancellationToken: token,
            commandType: CommandType.StoredProcedure
        ));
        
        return result > 0;
    }
}