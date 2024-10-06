using FluentValidation;
using Schedulify.App.Dtos;
using Schedulify.App.Repositories;

namespace Schedulify.App.Validators;

public abstract class CalendarBaseValidator<TDto>: AbstractValidator<TDto>
    where TDto: AbstractBaseCalendarDto
{
    private readonly IUserRepository _userRepository;
    private readonly ICalendarRepository _calendarRepository;
    
    protected CalendarBaseValidator(
        IUserRepository userRepository,
        ICalendarRepository calendarRepository)
    {
        _userRepository = userRepository;
        _calendarRepository = calendarRepository;
        
        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x => x.OwnerId)
            .MustAsync(OwnerIdExists)
            .WithMessage("Owner does not exist.");

        RuleFor(x => x.UpdatedAt)
            .NotEmpty();
    }
    
    private async Task<bool> OwnerIdExists(Guid id, CancellationToken token)
    {
        return !await _userRepository.ExistsByIdAsync(id, token);
    }
    
    protected async Task<bool> IdExists(Guid id, CancellationToken token)
    {
        return !await _calendarRepository.ExistsByIdAsync(id, token);
    }
}

public class CreateCalendarDtoValidator: CalendarBaseValidator<CreateCalendarDto>
{
    public CreateCalendarDtoValidator(
        IUserRepository userRepository,
        ICalendarRepository calendarRepository) : base(userRepository, calendarRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, token) => !await IdExists(id, token))
            .WithMessage("Calendar with this id already exists.");
        
        RuleFor(x => x.CreatedAt)
            .NotEmpty()
            .Must((dto, createdAt) => createdAt <= dto.UpdatedAt)
            .WithMessage("The creation date cannot be later than the updated date.");
    }
}

public class UpdateCalendarDtoValidator: CalendarBaseValidator<UpdateCalendarDto>
{
    public UpdateCalendarDtoValidator(
        IUserRepository userRepository,
        ICalendarRepository calendarRepository) : base(userRepository, calendarRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(IdExists)
            .WithMessage("Calendar with this id does not exist.");
    }
}