namespace Schedulify.Contracts.Requests;

public class CreateCategoryRequest
{
    public required string Name { get; init; }
    
    public Guid? AuthorId { get; init; }
}

public class UpdateCategoryRequest
{
    public required Guid Id { get; init; }
    
    public required string Name { get; init; }
    
    public Guid AuthorId { get; init; }
}