using Schedulify.App.Attributes;
using Schedulify.App.Entities;
using Schedulify.App.Enums;

namespace Schedulify.App.Repositories;

[Injectable(InjectableTypeEnum.Singleton, typeof(CategoryRepository))]
public interface ICategoryRepository
{
    public Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<CategoryEntity?> GetByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<IEnumerable<CategoryEntity>> GetByOwnerIdAsync(Guid id, CancellationToken token = default);
    
    public Task<bool> CreateAsync(CategoryEntity category, CancellationToken token = default);
    
    public Task<bool> UpdateAsync(CategoryEntity category, CancellationToken token = default);
    
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
}

public class CategoryRepository: ICategoryRepository
{
    private readonly List<CategoryEntity> _categories; //TODO: Replace this with a real database

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default)
    {
        return false;
    }

    public async Task<CategoryEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return _categories.SingleOrDefault(x => x.Id == id);
    }
    
    public async Task<IEnumerable<CategoryEntity>> GetByOwnerIdAsync(Guid id, CancellationToken token = default)
    {
        return this._categories.AsEnumerable();
    }

    public async Task<bool> CreateAsync(CategoryEntity category, CancellationToken token = default)
    {
        return true;
    }

    public async Task<bool> UpdateAsync(CategoryEntity category, CancellationToken token = default)
    {
        return true;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        return true;
    }
}