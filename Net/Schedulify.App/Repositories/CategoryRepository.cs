using Dapper;
using Schedulify.App.Attributes;
using Schedulify.App.Database;
using Schedulify.App.Dtos;
using Schedulify.App.Entities;
using Schedulify.App.Enums;

namespace Schedulify.App.Repositories;

[Injectable(InjectableTypeEnum.Singleton, typeof(CategoryRepository))]
public interface ICategoryRepository
{
    public Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<CategoryEntity?> GetByIdAsync(Guid id, CancellationToken token = default);
    
    public Task<IEnumerable<CategoryEntity>> GetByOwnerIdAsync(Guid id, CancellationToken token = default);
    
    public Task<bool> CreateAsync(CreateCategoryDto category, CancellationToken token = default);
    
    public Task<bool> UpdateAsync(UpdateCategoryDto category, CancellationToken token = default);
    
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);
}

public class CategoryRepository : ICategoryRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    
    public CategoryRepository(IDbConnectionFactory dbConnectionFactory)
    {
        this._dbConnectionFactory = dbConnectionFactory;
    }
    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        
        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(
            "SELECT COUNT(1) FROM Categories WHERE Id = @Id",
            new { Id = id },
            cancellationToken: token
        ));
    }

    public async Task<CategoryEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        
        return await connection.QuerySingleOrDefaultAsync<CategoryEntity>(new CommandDefinition(
            "SELECT * FROM Categories WHERE Id = @Id",
            new { Id = id },
            cancellationToken: token
        ));
    }
    
    public async Task<IEnumerable<CategoryEntity>> GetByOwnerIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);

        return await connection.QueryAsync<CategoryEntity>(new CommandDefinition(
            "SELECT * FROM Categories WHERE OwnerId = @Id",
            new { Id = id },
            cancellationToken: token
        ));
    }

    public async Task<bool> CreateAsync(CreateCategoryDto category, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();

        var result = await connection.ExecuteAsyncTransaction(new CommandDefinition(
            "INSERT INTO Categories (Id, Name, OwnerId, CreatedAt, UpdatedAt) VALUES (@Id, @Name, @OwnerId, @CreatedAt, @UpdatedAt)",
            category,
            transaction: transaction,
            cancellationToken: token
        ));
        
        return result > 0;
    }

    public async Task<bool> UpdateAsync(UpdateCategoryDto category, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        
        var result = await connection.ExecuteAsyncTransaction(new CommandDefinition(
            "UPDATE Categories SET Name = @Name, OwnerId = @OwnerId, UpdatedAt = @UpdatedAt WHERE Id = @Id",
            category,
            transaction: transaction,
            cancellationToken: token
        ));
        
        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync(token);
        using var transaction = connection.BeginTransaction();
        
        var result = await connection.ExecuteAsyncTransaction(new CommandDefinition(
            "DELETE FROM Categories WHERE Id = @Id",
            new { Id = id },
            transaction: transaction,
            cancellationToken: token
        ));
        
        return result > 0;
    }
}