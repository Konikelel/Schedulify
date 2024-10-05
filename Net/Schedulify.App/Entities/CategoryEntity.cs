namespace Schedulify.App.Entities;

public class CategoryEntity
{
    public required Guid Id { get; init; }
    
    public required string Name { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }
    
    public required DateTimeOffset UpdatedAt { get; init; }
    
    public required Guid OwnerId { get; init; }
}