namespace Schedulify.App.Models;

public class CategoryModel
{
    public required Guid Id { get; init; }
    
    public required string Name { get; set; }
    
    public required Guid AuthorId { get; set; }
}