using FluentValidation;
using Schedulify.App.Dtos;
using Schedulify.App.Repositories;

namespace Schedulify.App.Validators;

public abstract class CategoryBaseValidator<TDto> : AbstractValidator<TDto>
    where TDto : AbstractBaseCategoryDto
{
    private readonly IUserRepository _userRepository;
    private readonly ICategoryRepository _categoryRepository;
    
    protected CategoryBaseValidator(
        IUserRepository userRepository,
        ICategoryRepository categoryRepository)
    {
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
        
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
        return !await _categoryRepository.ExistsByIdAsync(id, token);
    }
}

public class CreateCategoryDtoValidator : CategoryBaseValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator(
        IUserRepository userRepository,
        ICategoryRepository categoryRepository) : base(userRepository, categoryRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, token) => !await IdExists(id, token))
            .WithMessage("Category with this id already exists.");
        
        RuleFor(x => x.CreatedAt)
            .NotEmpty()
            .Must((dto, createdAt) => createdAt <= dto.UpdatedAt)
            .WithMessage("The creation date cannot be later than the updated date.");
    }
}

public class UpdateCategoryDtoValidator : CategoryBaseValidator<UpdateCategoryDto>
{
    public UpdateCategoryDtoValidator(
        IUserRepository userRepository,
        ICategoryRepository categoryRepository) : base(userRepository, categoryRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(IdExists)
            .WithMessage("Category with this id does not exist.");
    }
}