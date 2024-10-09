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
    
    public Task<IEnumerable<CategoryModel>> GetByOwnerIdAsync(Guid ownerId, CancellationToken token = default);
    
    public Task<bool> CreateAsync(CreateCategoryDto categoryDto, CancellationToken token = default);
    
    public Task<CategoryModel?> UpdateAsync(UpdateCategoryDto categoryDto, CancellationToken token = default);
    
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
}

public class CategoryService: ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IValidator<CreateCategoryDto> _createCategoryDtoValidator;
    private readonly IValidator<UpdateCategoryDto> _updateCategoryDtoValidator;
    private readonly IMapper _mapper;
    
    public CategoryService(
        ICategoryRepository categoryRepository,
        IScheduleRepository scheduleRepository,
        IValidator<CreateCategoryDto> createCategoryDtoValidator,
        IValidator<UpdateCategoryDto> updateCategoryDtoValidator,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _scheduleRepository = scheduleRepository;
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

    public async Task<IEnumerable<CategoryModel>> GetByOwnerIdAsync(Guid ownerId, CancellationToken token = default)
    {
        var entities = await _categoryRepository.GetByOwnerIdAsync(ownerId, token);
        var models = _mapper.Map<IEnumerable<CategoryModel>>(entities);
        return models;
    }

    public async Task<bool> CreateAsync(CreateCategoryDto categoryDto, CancellationToken token = default)
    {
        await _createCategoryDtoValidator.ValidateAndThrowAsync(categoryDto, token);
        return await _categoryRepository.CreateAsync(categoryDto, token);
    }

    public async Task<CategoryModel?> UpdateAsync(UpdateCategoryDto categoryDto, CancellationToken token = default)
    {
        await _updateCategoryDtoValidator.ValidateAndThrowAsync(categoryDto, token);
        var result = await _categoryRepository.UpdateAsync(categoryDto, token);
        
        return result ? await GetByIdAsync(categoryDto.Id, token) : null;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        return await _categoryRepository.DeleteByIdAsync(id, token) &&
               await _scheduleRepository.UpdateCategoryIdAsync(id, null, token);
    }
}