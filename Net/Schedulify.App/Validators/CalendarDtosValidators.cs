using FluentValidation;
using Schedulify.App.Dtos;
using Schedulify.App.Repositories;

namespace Schedulify.App.Validators;

public abstract class CalendarBaseValidator<TDto>: AbstractValidator<TDto>
    where TDto: CalendarBaseDto
{
    protected readonly ICalendarRepository CalendarRepository;
    
    protected CalendarBaseValidator(ICalendarRepository calendarRepository)
    {
        CalendarRepository = calendarRepository;
        
        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x => x.OwnerId)
            .MustAsync(ValidateOwnerId)
            .WithMessage("Owner does not exist.");

        RuleFor(x => x.UpdatedAt)
            .NotEmpty();
    }
    
    private async Task<bool> ValidateOwnerId(Guid id, CancellationToken token)
    {
        return !await CalendarRepository.ExistsByIdAsync(id, token);
    }
}

public class CreateCalendarDtoValidator: CalendarBaseValidator<CreateCalendarDto>
{
    public CreateCalendarDtoValidator(ICalendarRepository calendarRepository) : base(calendarRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(ValidateId)
            .WithMessage("Calendar with this id already exists.");
        
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

public class UpdateCalendarDtoValidator: CalendarBaseValidator<UpdateCalendarDto>
{
    public UpdateCalendarDtoValidator(ICalendarRepository calendarRepository) : base(calendarRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(ValidateId)
            .WithMessage("Calendar with this id does not exist.");
    }
    
    private async Task<bool> ValidateId(Guid id, CancellationToken token)
    {
        return await CalendarRepository.ExistsByIdAsync(id, token);
    }
}