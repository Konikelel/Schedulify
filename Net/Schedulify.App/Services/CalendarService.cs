using Schedulify.App.Attributes;
using Schedulify.App.Dtos;
using Schedulify.App.Entities;
using Schedulify.App.Enums;
using Schedulify.App.Models;
using Schedulify.App.Repositories;

namespace Schedulify.App.Services;

[Injectable(InjectableTypeEnum.Singleton, typeof(CalendarService))]
public interface ICalendarService
{
    public Task<CalendarModel?> GetByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<IEnumerable<CalendarModel>> GetByOwnerIdAsync(Guid id, CancellationToken token = default);
    
    public Task<bool> CreateAsync(CreateCalendarDto calendar, CancellationToken token = default);
    
    public Task<CalendarModel?> UpdateAsync(UpdateCalendarDto calendar, CancellationToken token = default);
    
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
}

public class CalendarService : ICalendarService
{
    private readonly ICalendarRepository _calendarRepository;
    
    public async Task<CalendarModel?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CalendarModel>> GetByOwnerIdAsync(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateAsync(CreateCalendarDto calendar, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public async Task<CalendarModel?> UpdateAsync(UpdateCalendarDto calendar, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}