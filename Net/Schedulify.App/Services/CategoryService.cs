using AutoMapper;
using FluentValidation;
using Schedulify.App.Attributes;
using Schedulify.App.Dtos;
using Schedulify.App.Enums;
using Schedulify.App.Models;
using Schedulify.App.Repositories;

namespace Schedulify.App.Services;

[Injectable(InjectableTypeEnum.Singleton, typeof(CategoryService))]
public interface ICategoryService
{
    public Task<CategoryModel?> GetByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<IEnumerable<CategoryModel>> GetByOwnerIdAsync(Guid id, CancellationToken token = default);
    
    public Task<bool> CreateAsync(CreateCategoryDto category, CancellationToken token = default);
    
    public Task<CategoryModel?> UpdateAsync(UpdateCategoryDto category, CancellationToken token = default);
    
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
}

public class CategoryService: ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<CreateCategoryDto> _createCategoryDtoValidator;
    private readonly IValidator<UpdateCategoryDto> _updateCategoryDtoValidator;
    private readonly IMapper _mapper;
    
    public CategoryService(
        ICategoryRepository categoryRepository,
        IValidator<CreateCategoryDto> createCategoryDtoValidator,
        IValidator<UpdateCategoryDto> updateCategoryDtoValidator,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _createCategoryDtoValidator = createCategoryDtoValidator;
        _updateCategoryDtoValidator = updateCategoryDtoValidator;
        _mapper = mapper;
    }
    
    public async Task<CategoryModel?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var entity = await _categoryRepository.GetByIdAsync(id, token);
        var model = _mapper.Map<CategoryModel>(entity);
        return model;
    }

    public async Task<IEnumerable<CategoryModel>> GetByOwnerIdAsync(Guid id, CancellationToken token = default)
    {
        var entities = await _categoryRepository.GetByOwnerIdAsync(id, token);
        var models = _mapper.Map<IEnumerable<CategoryModel>>(entities);
        return models;
    }

    public async Task<bool> CreateAsync(CreateCategoryDto category, CancellationToken token = default)
    {
        await _createCategoryDtoValidator.ValidateAndThrowAsync(category, token);
        return await _categoryRepository.CreateAsync(category, token);
    }

    public async Task<CategoryModel?> UpdateAsync(UpdateCategoryDto category, CancellationToken token = default)
    {
        await _updateCategoryDtoValidator.ValidateAndThrowAsync(category, token);
        var result = await _categoryRepository.UpdateAsync(category, token);
        
        return result ? await GetByIdAsync(category.Id, token) : null;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        return await _categoryRepository.DeleteByIdAsync(id, token);
    }
}