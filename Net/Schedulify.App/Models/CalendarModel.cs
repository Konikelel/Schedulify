namespace Schedulify.App.Models;

public class CalendarModel
{
    public required Guid Id { get; init; }
    
    public required string Name { get; set; }
    
    public required DateTimeOffset CreatedAt { get; init; }

    public required DateTimeOffset UpdatedAt { get; set; }
    
    public required Guid OwnerId { get; set; }
    
    public IEnumerable<CategoryModel> Categories { get; set; } = Enumerable.Empty<CategoryModel>();
    
    public IEnumerable<ScheduleModel> Schedules { get; set; } = Enumerable.Empty<ScheduleModel>();
}