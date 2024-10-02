namespace Schedulify.App.Models;

public class CategoryModel
{
    public required Guid Id { get; init; }
    
    public required string Name { get; set; }
    
    public required DateTimeOffset CreatedAt { get; set; }
    
    public required DateTimeOffset UpdatedAt { get; set; }
    
    public required Guid AuthorId { get; set; }
    
    public required IEnumerable<CategoryModel> Categories { get; set; }
    
    public required IEnumerable<ScheduleModel> Schedules { get; set; }
}