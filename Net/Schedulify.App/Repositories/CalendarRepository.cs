using Schedulify.App.Attributes;
using Schedulify.App.Entities;
using Schedulify.App.Enums;

namespace Schedulify.App.Repositories;

[Injectable(InjectableTypeEnum.Singleton, typeof(CalendarRepository))]
public interface ICalendarRepository
{
    public Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<CalendarEntity?> GetByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<IEnumerable<CalendarEntity>> GetByOwnerIdAsync(Guid id, CancellationToken token = default);
    
    public Task<bool> CreateAsync(CalendarEntity calendar, CancellationToken token = default);
    
    public Task<bool> UpdateAsync(CalendarEntity calendar, CancellationToken token = default);
    
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
}

public class CalendarRepository: ICalendarRepository
{
    private readonly List<CalendarEntity> _calendar; //TODO: Replace this with a real database

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default)
    {
        return false;
    }

    public async Task<CalendarEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return _calendar.SingleOrDefault(x => x.Id == id);
    }
    
    public async Task<IEnumerable<CalendarEntity>> GetByOwnerIdAsync(Guid id, CancellationToken token = default)
    {
        return this._calendar.AsEnumerable();
    }

    public async Task<bool> CreateAsync(CalendarEntity calendar, CancellationToken token = default)
    {
        return true;
    }

    public async Task<bool> UpdateAsync(CalendarEntity calendar, CancellationToken token = default)
    {
        return true;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        return true;
    }
}