namespace Schedulify.App.Entities;

public class CategoryEnum
{
    public required Guid Id { get; init; }
    
    public required string Name { get; set; }
    
    public required DateTimeOffset CreatedAt { get; set; }
    
    public required DateTimeOffset UpdatedAt { get; set; }
    
    public required Guid AuthorId { get; set; }
}