using FluentValidation;
using Schedulify.App.Dtos;
using Schedulify.App.Repositories;

namespace Schedulify.App.Validators;

public abstract class UserBaseValidator<TDto> : AbstractValidator<TDto>
    where TDto : AbstractBaseUserDtos
{
    private readonly IUserRepository _userRepository;
    
    protected UserBaseValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Username)
            .NotEmpty()
            .MaximumLength(64)
            .MustAsync(UsernameNotExists)
            .WithMessage("User with this username already exists.");
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(64)
            .MustAsync(EmailNotExists)
            .WithMessage("User with this email already exists.");
        
        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(x => x.PasswordHash)
            .NotEmpty()
            .Must(hash => hash.Length <= 64)
            .WithMessage("Password hash is too long.");
        
        RuleFor(x => x.PasswordSalt)
            .NotEmpty()
            .Must(hash => hash.Length <= 128)
            .WithMessage("Password salt is too long.");

        RuleFor(x => x.UpdatedAt)
            .NotEmpty();
    }
    
    private async Task<bool> UsernameNotExists(string username, CancellationToken token)
    {
        return !await _userRepository.ExistsByUsernameAsync(username, token);
    }
    
    private async Task<bool> EmailNotExists(string email, CancellationToken token)
    {
        return !await _userRepository.ExistsByEmailAsync(email, token);
    }

    protected async Task<bool> IdExists(Guid id, CancellationToken token)
    {
        return await _userRepository.ExistsByIdAsync(id, token);
    }
}

public class CreateUserDtoValidator : UserBaseValidator<CreateUserDto>
{
    public CreateUserDtoValidator(IUserRepository userRepository) : base(userRepository)
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

public class UpdateUserDtoValidator : UserBaseValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator(IUserRepository userRepository) : base(userRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(IdExists)
            .WithMessage("Calendar with this id does not exist.");
    }
}