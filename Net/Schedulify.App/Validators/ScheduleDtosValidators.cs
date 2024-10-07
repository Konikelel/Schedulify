using FluentValidation;
using Schedulify.App.Dtos;
using Schedulify.App.Repositories;

namespace Schedulify.App.Validators;

public abstract class ScheduleBaseValidator<TDto> : AbstractValidator<TDto>
    where TDto: AbstractBaseScheduleDto
{
    private readonly IUserRepository _userRepository;
    private readonly ICalendarRepository _calendarRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IScheduleRepository _scheduleRepository;

    protected ScheduleBaseValidator(
        IUserRepository userRepository,
        ICalendarRepository calendarRepository,
        ICategoryRepository categoryRepository,
        IScheduleRepository scheduleRepository)
    {
        _userRepository = userRepository;
        _calendarRepository = calendarRepository;
        _categoryRepository = categoryRepository;
        _scheduleRepository = scheduleRepository;
        
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.CalendarId)
            .NotEmpty()
            .MustAsync(CalendarIdExists)
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
            .MustAsync(CategoryIdValid)
            .WithMessage("Category does not exist.");

        RuleFor(x => x.OwnerId)
            .MustAsync(OwnerIdExists)
            .WithMessage("Owner does not exist.");

        RuleFor(x => x.UpdatedAt)
            .NotEmpty();
    }
    
    private async Task<bool> OwnerIdExists(Guid id, CancellationToken token)
    {
        return await _userRepository.ExistsByIdAsync(id, token);
    }

    private async Task<bool> CalendarIdExists(Guid id, CancellationToken token)
    {
        return await _calendarRepository.ExistsByIdAsync(id, token);
    }
    
    private async Task<bool> CategoryIdValid(Guid? id, CancellationToken token)
    {
        return id == null || await _categoryRepository.ExistsByIdAsync(id.Value, token);
    }
    
    protected async Task<bool> IdExists(Guid id, CancellationToken token)
    {
        return await _scheduleRepository.ExistsByIdAsync(id, token);
    }
}

public class CreateScheduleDtoValidator: ScheduleBaseValidator<CreateScheduleDto>
{
    public CreateScheduleDtoValidator(
        IUserRepository userRepository,
        ICalendarRepository calendarRepository,
        ICategoryRepository categoryRepository,
        IScheduleRepository scheduleRepository) : base(userRepository, calendarRepository, categoryRepository, scheduleRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, token) => !await IdExists(id, token))
            .WithMessage("Schedule with this id already exists.");
        
        RuleFor(x => x.CreatedAt)
            .NotEmpty()
            .Must((dto, createdAt) => createdAt <= dto.UpdatedAt)
            .WithMessage("The creation date cannot be later than the updated date.");
    }
}

public class UpdateScheduleDtoValidator: ScheduleBaseValidator<UpdateScheduleDto>
{
    public UpdateScheduleDtoValidator(
        IUserRepository userRepository,
        ICalendarRepository calendarRepository,
        ICategoryRepository categoryRepository,
        IScheduleRepository scheduleRepository) : base(userRepository, calendarRepository, categoryRepository, scheduleRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(IdExists)
            .WithMessage("Schedule with this id does not exist.");
    }
}