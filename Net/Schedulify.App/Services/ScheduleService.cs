using Schedulify.App.Attributes;
using Schedulify.App.Dtos;
using Schedulify.App.Enums;
using Schedulify.App.Models;

namespace Schedulify.App.Services;

[Injectable(InjectableTypeEnum.Singleton, typeof(ScheduleService))]
public interface IScheduleService
{
    public Task<ScheduleModel?> GetByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<IEnumerable<ScheduleModel>> GetByOwnerIdAsync(Guid id, CancellationToken token = default);
    
    public Task<bool> CreateAsync(CreateScheduleDto schedule, CancellationToken token = default);
    
    public Task<ScheduleModel?> UpdateAsync(UpdateScheduleDto schedule, CancellationToken token = default);
    
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
}

public class ScheduleService: IScheduleService
{
    public Task<ScheduleModel?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ScheduleModel>> GetByOwnerIdAsync(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateAsync(CreateScheduleDto schedule, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<ScheduleModel?> UpdateAsync(UpdateScheduleDto schedule, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}