using FluentValidation;
using Schedulify.App.Dtos;
using Schedulify.App.Repositories;

namespace Schedulify.App.Validators;

public abstract class ScheduleBaseValidator<TDto> : AbstractValidator<TDto>
    where TDto: AbstractBaseScheduleDto
{
    protected readonly ICalendarRepository CalendarRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IScheduleRepository _scheduleRepository;

    protected ScheduleBaseValidator(
        ICalendarRepository calendarRepository,
        ICategoryRepository categoryRepository,
        IScheduleRepository scheduleRepository)
    {
        CalendarRepository = calendarRepository;
        _categoryRepository = categoryRepository;
        _scheduleRepository = scheduleRepository;
        
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.CalendarId)
            .NotEmpty()
            .MustAsync(ValidateCalendarId)
            .WithMessage("Calendar does not exist.");
        
        RuleFor(x => x.TimeStart)
            .NotEmpty();
        
        RuleFor(x => x.TimeEnd)
            .NotEmpty()
            .GreaterThan(x => x.TimeStart)
            .WithMessage("End time must be greater than start time.");
        
        RuleFor(x => x.Frequency)
            .NotEmpty()
            .IsInEnum();
        
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(64);
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(256);
        
        RuleFor(x => x.Link)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .MustAsync(ValidateCategoryId)
            .WithMessage("Category does not exist.");

        RuleFor(x => x.OwnerId)
            .MustAsync(ValidateOwnerId)
            .WithMessage("Owner does not exist.");

        RuleFor(x => x.UpdatedAt)
            .NotEmpty();
    }

    private async Task<bool> ValidateCalendarId(Guid id, CancellationToken token)
    {
        return await CalendarRepository.ExistsByIdAsync(id, token);
    }
    
    private async Task<bool> ValidateCategoryId(Guid id, CancellationToken token)
    {
        return await _categoryRepository.ExistsByIdAsync(id, token);
    }

    private async Task<bool> ValidateOwnerId(Guid id, CancellationToken token)
    {
        return !await _scheduleRepository.ExistsByIdAsync(id, token);
    }
}

public class CreateScheduleDtoValidator: ScheduleBaseValidator<CreateScheduleDto>
{
    public CreateScheduleDtoValidator(
        ICalendarRepository calendarRepository,
        ICategoryRepository categoryRepository,
        IScheduleRepository scheduleRepository) : base(calendarRepository, categoryRepository, scheduleRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(ValidateId)
            .WithMessage("Schedule with this id already exists.");
        
        RuleFor(x => x.CreatedAt)
            .NotEmpty()
            .Must((dto, createdAt) => createdAt <= dto.UpdatedAt)
            .WithMessage("The creation date cannot be later than the updated date.");
    }
    
    private async Task<bool> ValidateId(Guid id, CancellationToken token)
    {
        return !await CalendarRepository.ExistsByIdAsync(id, token);
    }
}

public class UpdateScheduleDtoValidator: ScheduleBaseValidator<UpdateScheduleDto>
{
    public UpdateScheduleDtoValidator(
        ICalendarRepository calendarRepository,
        ICategoryRepository categoryRepository,
        IScheduleRepository scheduleRepository) : base(calendarRepository, categoryRepository, scheduleRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(ValidateId)
            .WithMessage("Schedule with this id does not exist.");
    }
    
    private async Task<bool> ValidateId(Guid id, CancellationToken token)
    {
        return await CalendarRepository.ExistsByIdAsync(id, token);
    }
}