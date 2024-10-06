using Schedulify.App.Attributes;
using Schedulify.App.Entities;
using Schedulify.App.Enums;

namespace Schedulify.App.Repositories;

[Injectable(InjectableTypeEnum.Singleton, typeof(UserRepository))]
public interface IUserRepository
{
    public Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default);

    public Task<bool> ExistsByUsernameAsync(string username, CancellationToken token = default);

    public Task<bool> ExistsByEmailAsync(string email, CancellationToken token = default);
    
    public Task<UserEntity?> GetByIdAsync(Guid id, CancellationToken token = default);

    public Task<bool> CreateAsync(UserEntity user, CancellationToken token = default);
    
    public Task<bool> UpdateAsync(UserEntity user, CancellationToken token = default);
    
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
}

public class UserRepository : IUserRepository
{
    private readonly List<UserEntity> _users; //TODO: Replace this with a real database

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default)
    {
        return false;
    }

    public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken token = default)
    {
        return false;
    }
    
    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken token = default)
    {
        return false;
    }

    public async Task<UserEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return _users.SingleOrDefault(x => x.Id == id);
    }

    public async Task<bool> CreateAsync(UserEntity user, CancellationToken token = default)
    {
        return true;
    }

    public async Task<bool> UpdateAsync(UserEntity user, CancellationToken token = default)
    {
        return true;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        return true;
    }
}