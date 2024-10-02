namespace Schedulify.App.Entities;

public class CategoryEnum
{
    public required Guid Id { get; init; }
    
    public required string Name { get; set; }

    public DateTimeOffset? CreatedAt { get; init; }
    
    public DateTimeOffset? UpdatedAt { get; init; }
    
    public required Guid AuthorId { get; set; }
}