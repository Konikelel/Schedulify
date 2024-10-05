using Schedulify.App.Attributes;
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
    
    public Task<bool> CreateAsync(ScheduleEntity schedule, CancellationToken token = default);
    
    public Task<bool> UpdateAsync(ScheduleEntity schedule, CancellationToken token = default);
    
    public Task<bool> UpdateCategoryIdAsync(Guid id, Guid newId, CancellationToken token = default);
    
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<bool> DeleteByCalendarIdAsync(Guid id, CancellationToken token = default);
}

public class ScheduleRepository : IScheduleRepository
{
    private readonly List<ScheduleEntity> _schedules; //TODO: Replace this with a real database

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default)
    {
        return false;
    }

    public async Task<ScheduleEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return _schedules.SingleOrDefault(x => x.Id == id);
    }

    public async Task<IEnumerable<ScheduleEntity>> GetByCalendarIdAsync(Guid id, CancellationToken token = default)
    {
        return this._schedules.AsEnumerable();
    }
    
    public async Task<IEnumerable<ScheduleEntity>> GetByCategoryIdAsync(Guid id, CancellationToken token = default)
    {
        return this._schedules.AsEnumerable();
    }
    
    public async Task<IEnumerable<ScheduleEntity>> GetByOwnerIdAsync(Guid id, CancellationToken token = default)
    {
        return this._schedules.AsEnumerable();
    }

    public async Task<bool> CreateAsync(ScheduleEntity schedule, CancellationToken token = default)
    {
        return true;
    }

    public async Task<bool> UpdateAsync(ScheduleEntity schedule, CancellationToken token = default)
    {
        return true;
    }
    
    public async Task<bool> UpdateCategoryIdAsync(Guid id, Guid newId, CancellationToken token = default)
    {
        return true;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        return true;
    }
    
    public async Task<bool> DeleteByCalendarIdAsync(Guid id, CancellationToken token = default)
    {
        return true;
    }
}