using Schedulify.App.Models;

namespace Schedulify.App.Repositories;

public interface IScheduleRepository
{
    public Task<ScheduleModel?> GetByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<IEnumerable<ScheduleModel>> GetByAuthorAsync(Guid id, CancellationToken token = default);
    
    public Task<bool> CreateAsync(ScheduleModel schedule, CancellationToken token = default);
    
    public Task<bool> UpdateAsync(ScheduleModel schedule, CancellationToken token = default);
    
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
}

public class ScheduleRepository : IScheduleRepository
{
    // ADD CONNECTION DJ
    private readonly List<ScheduleModel> _schedules;

    public Task<ScheduleModel?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var schedule = _schedules.SingleOrDefault(x => x.Id == id);
        return Task.FromResult(schedule);
    }

    public Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default)
    {
        return Task.FromResult(true);       
    }

    public Task<IEnumerable<ScheduleModel>> GetByAuthorAsync(Guid id, CancellationToken token = default)
    {
        return Task.FromResult(_schedules.AsEnumerable());;      
    }

    public Task<bool> CreateAsync(ScheduleModel schedule, CancellationToken token = default)
    {
        return Task.FromResult(true);       
    }

    public Task<bool> UpdateAsync(ScheduleModel schedule, CancellationToken token = default)
    {
        return Task.FromResult(true);       
    }

    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        return Task.FromResult(true);       
    }
}