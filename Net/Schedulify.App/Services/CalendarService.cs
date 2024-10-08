using AutoMapper;
using FluentValidation;
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
    
    public Task<IEnumerable<CalendarModel>> GetByOwnerIdAsync(Guid ownerId, CancellationToken token = default);
    
    public Task<bool> CreateAsync(CreateCalendarDto calendarDto, CancellationToken token = default);
    
    public Task<CalendarModel?> UpdateAsync(UpdateCalendarDto calendarDto, CancellationToken token = default);
    
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
}

public class CalendarService : ICalendarService
{
    private readonly ICalendarRepository _calendarRepository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IValidator<CreateCalendarDto> _createCalendarDtoValidator;
    private readonly IValidator<UpdateCalendarDto> _updateCalendarDtoValidator;
    private readonly IMapper _mapper;
    
    public CalendarService(
        ICalendarRepository calendarRepository,
        IScheduleRepository scheduleRepository,
        IValidator<CreateCalendarDto> createCalendarDtoValidator,
        IValidator<UpdateCalendarDto> updateCalendarDtoValidator,
        IMapper mapper)
    {
        _calendarRepository = calendarRepository;
        _scheduleRepository = scheduleRepository;
        _createCalendarDtoValidator = createCalendarDtoValidator;
        _updateCalendarDtoValidator = updateCalendarDtoValidator;
        _mapper = mapper;
    }
    
    public async Task<CalendarModel?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var entity = await _calendarRepository.GetByIdAsync(id, token);
        var model = _mapper.Map<CalendarModel>(entity);
        return model;
    }

    public async Task<IEnumerable<CalendarModel>> GetByOwnerIdAsync(Guid ownerId, CancellationToken token = default)
    {
        var entities = await _calendarRepository.GetByOwnerIdAsync(ownerId, token);
        var models = _mapper.Map<IEnumerable<CalendarModel>>(entities);
        return models;
    }

    public async Task<bool> CreateAsync(CreateCalendarDto calendarDto, CancellationToken token = default)
    {
        await _createCalendarDtoValidator.ValidateAndThrowAsync(calendarDto, token);
        return await _calendarRepository.CreateAsync(calendarDto, token);
    }

    public async Task<CalendarModel?> UpdateAsync(UpdateCalendarDto calendarDto, CancellationToken token = default)
    {
        await _updateCalendarDtoValidator.ValidateAndThrowAsync(calendarDto, token);
        var result = await _calendarRepository.UpdateAsync(calendarDto, token);
        
        return result ? await GetByIdAsync(calendarDto.Id, token) : null;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        return await _calendarRepository.DeleteByIdAsync(id, token) &&
               await _scheduleRepository.DeleteByCalendarIdAsync(id, token);
    }
}