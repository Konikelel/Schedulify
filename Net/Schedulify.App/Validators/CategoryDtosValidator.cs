using FluentValidation;
using Schedulify.App.Dtos;
using Schedulify.App.Repositories;

namespace Schedulify.App.Validators;

public abstract class CategoryBaseValidator<TDto>: AbstractValidator<TDto>
    where TDto: CategoryBaseDto
{
    protected readonly ICategoryRepository CategoryRepository;
    
    protected CategoryBaseValidator(ICategoryRepository categoryRepository)
    {
        CategoryRepository = categoryRepository;
        
        RuleFor(x => x.Id)
            .NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x => x.OwnerId)
            .MustAsync(ValidateOwnerId)
            .WithMessage("Owner does not exist.");
    }
    
    private async Task<bool> ValidateOwnerId(Guid id, CancellationToken token)
    {
        return !await CategoryRepository.ExistsByIdAsync(id, token);
    }
}

public class CreateCategoryDtoValidator: CategoryBaseValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator(ICategoryRepository categoryRepository) : base(categoryRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(ValidateId)
            .WithMessage("Category with this id already exists.");
    }
    
    private async Task<bool> ValidateId(Guid id, CancellationToken token)
    {
        return !await CategoryRepository.ExistsByIdAsync(id, token);
    }
}

public class UpdateCategoryDtoValidator: CategoryBaseValidator<UpdateCategoryDto>
{
    public UpdateCategoryDtoValidator(ICategoryRepository categoryRepository) : base(categoryRepository)
    {
        RuleFor(x => x.Id)
            .MustAsync(ValidateId)
            .WithMessage("Category with this id does not exist.");
    }
    
    private async Task<bool> ValidateId(Guid id, CancellationToken token)
    {
        return await CategoryRepository.ExistsByIdAsync(id, token);
    }
}