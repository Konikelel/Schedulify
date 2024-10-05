using FluentValidation;
using Schedulify.App.Dtos;
using Schedulify.App.Repositories;

namespace Schedulify.App.Validators;

public abstract class UserBaseValidator<TDto>: AbstractValidator<TDto>
    where TDto: UserBaseDtos
{
    protected readonly IUserRepository UserRepository;
    
    protected UserBaseValidator(IUserRepository userRepository)
    {
        UserRepository = userRepository;
        
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Username)
            .NotEmpty()
            .MaximumLength(64)
            .MustAsync(ValidateUsername)
            .WithMessage("User with this username already exists.");
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(64)
            .MustAsync(ValidateEmail)
            .WithMessage("User with this email already exists.");
        
        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .MaximumLength(256);
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(64);
    }
    
    private async Task<bool> ValidateUsername(string username, CancellationToken token)
    {
        return !await UserRepository.ExistsByUsernameAsync(username, token);
    }
    
    private async Task<bool> ValidateEmail(string email, CancellationToken token)
    {
        return !await UserRepository.ExistsByEmailAsync(email, token);
    }
}

public class CreateUserDtoValidator: UserBaseValidator<CreateUserDto>
{
    public CreateUserDtoValidator(IUserRepository userRepository) : base(userRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(ValidateId)
            .WithMessage("Calendar with this id already exists.");
    }
    
    private async Task<bool> ValidateId(Guid id, CancellationToken token)
    {
        return !await UserRepository.ExistsByIdAsync(id, token);
    }
}

public class UpdateUserDtoValidator: UserBaseValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator(IUserRepository userRepository) : base(userRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(ValidateId)
            .WithMessage("Calendar with this id does not exist.");
    }
    
    private async Task<bool> ValidateId(Guid id, CancellationToken token)
    {
        return await UserRepository.ExistsByIdAsync(id, token);
    }
}