using AutoMapper;
using FluentValidation;
using Schedulify.App.Attributes;
using Schedulify.App.Dtos;
using Schedulify.App.Enums;
using Schedulify.App.Models;
using Schedulify.App.Repositories;

namespace Schedulify.App.Services;

[Injectable(InjectableTypeEnum.Singleton, typeof(ScheduleService))]
public interface IScheduleService
{
    public Task<ScheduleModel?> GetByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<IEnumerable<ScheduleModel>> GetByOwnerIdAsync(Guid ownerId, CancellationToken token = default);
    
    public Task<bool> CreateAsync(CreateScheduleDto scheduleDto, CancellationToken token = default);
    
    public Task<ScheduleModel?> UpdateAsync(UpdateScheduleDto scheduleDto, CancellationToken token = default);
    
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
}

public class ScheduleService: IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IValidator<CreateScheduleDto> _createScheduleDtoValidator;
    private readonly IValidator<UpdateScheduleDto> _updateScheduleDtoValidator;
    private readonly IMapper _mapper;
    
    public ScheduleService(
        IScheduleRepository scheduleRepository,
        IValidator<CreateScheduleDto> createScheduleDtoValidator,
        IValidator<UpdateScheduleDto> updateScheduleDtoValidator,
        IMapper mapper)
    {
        _scheduleRepository = scheduleRepository;
        _createScheduleDtoValidator = createScheduleDtoValidator;
        _updateScheduleDtoValidator = updateScheduleDtoValidator;
        _mapper = mapper;
    }
    
    public async Task<ScheduleModel?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var entity = await _scheduleRepository.GetByIdAsync(id, token);
        var model = _mapper.Map<ScheduleModel>(entity);
        return model;
    }

    public async Task<IEnumerable<ScheduleModel>> GetByOwnerIdAsync(Guid ownerId, CancellationToken token = default)
    {
        var entities = await _scheduleRepository.GetByOwnerIdAsync(ownerId, token);
        var models = _mapper.Map<IEnumerable<ScheduleModel>>(entities);
        return models;
    }

    public async Task<bool> CreateAsync(CreateScheduleDto scheduleDto, CancellationToken token = default)
    {
        await _createScheduleDtoValidator.ValidateAndThrowAsync(scheduleDto, token);
        return await _scheduleRepository.CreateAsync(scheduleDto, token);
    }

    public async Task<ScheduleModel?> UpdateAsync(UpdateScheduleDto scheduleDto, CancellationToken token = default)
    {
        await _updateScheduleDtoValidator.ValidateAndThrowAsync(scheduleDto, token);
        var result = await _scheduleRepository.UpdateAsync(scheduleDto, token);
        
        return result ? await GetByIdAsync(scheduleDto.Id, token) : null;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        return await _scheduleRepository.DeleteByIdAsync(id, token);
    }
}